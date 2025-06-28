// ============================
//  Project: CustomerCommLib
// ============================
namespace CustomerCommLib
{
    public interface IMailSender
    {
        bool SendMail(string toAddress, string message);
    }

    public class MailSender : IMailSender
    {
        public bool SendMail(string toAddress, string message)
        {
            // Simulate sending an email
            return true; // would normally connect to SMTP
        }
    }

    public class CustomerComm
    {
        private readonly IMailSender _mailSender;

        public CustomerComm(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public bool SendMailToCustomer()
        {
            return _mailSender.SendMail("cust123@abc.com", "Some Message");
        }
    }
}


// ============================
//  Project: CustomerComm.Tests
// ============================
using NUnit.Framework;
using Moq;
using CustomerCommLib;

namespace CustomerComm.Tests
{
    [TestFixture]
    public class CustomerCommTests
    {
        private Mock<IMailSender> _mockMailSender;
        private CustomerComm _customerComm;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mockMailSender = new Mock<IMailSender>();
            _mockMailSender.Setup(m => m.SendMail(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _customerComm = new CustomerComm(_mockMailSender.Object);
        }

        [Test]
        public void SendMailToCustomer_ReturnsTrue_WhenMailIsSent()
        {
            var result = _customerComm.SendMailToCustomer();
            Assert.That(result, Is.True);
        }
    }
}


// ============================
//  Project: MagicFilesLib
// ============================
using System.Collections.Generic;
using System.IO;

namespace MagicFilesLib
{
    public interface IDirectoryExplorer
    {
        ICollection<string> GetFiles(string path);
    }

    public class DirectoryExplorer : IDirectoryExplorer
    {
        public ICollection<string> GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}


// ============================
//  Project: DirectoryExplorer.Tests
// ============================
using NUnit.Framework;
using Moq;
using MagicFilesLib;
using System.Collections.Generic;

namespace DirectoryExplorer.Tests
{
    [TestFixture]
    public class DirectoryExplorerTests
    {
        private Mock<IDirectoryExplorer> _mockExplorer;
        private readonly string _file1 = "file.txt";
        private readonly string _file2 = "file2.txt";

        [OneTimeSetUp]
        public void SetUp()
        {
            _mockExplorer = new Mock<IDirectoryExplorer>();
            _mockExplorer.Setup(m => m.GetFiles(It.IsAny<string>())).Returns(new List<string> { _file1, _file2 });
        }

        [Test]
        public void GetFiles_ShouldReturnMockedFileList()
        {
            var files = _mockExplorer.Object.GetFiles("C:/fakepath");
            Assert.That(files, Is.Not.Null);
            Assert.That(files.Count, Is.EqualTo(2));
            Assert.That(files, Contains.Item(_file1));
        }
    }
}


// ============================
//  Project: PlayersManagerLib
// ============================
using System;

namespace PlayersManagerLib
{
    public interface IPlayerMapper
    {
        bool IsPlayerNameExistsInDb(string name);
        void AddNewPlayerIntoDb(string name);
    }

    public class Player
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Country { get; private set; }
        public int NoOfMatches { get; private set; }

        public Player(string name, int age, string country, int noOfMatches)
        {
            Name = name;
            Age = age;
            Country = country;
            NoOfMatches = noOfMatches;
        }

        public static Player RegisterNewPlayer(string name, IPlayerMapper playerMapper)
        {
            if (playerMapper == null)
                throw new ArgumentNullException(nameof(playerMapper));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Player name can't be empty.");

            if (playerMapper.IsPlayerNameExistsInDb(name))
                throw new ArgumentException("Player name already exists.");

            playerMapper.AddNewPlayerIntoDb(name);

            return new Player(name, 23, "India", 30);
        }
    }
}


// ============================
//  Project: PlayerManager.Tests
// ============================
using NUnit.Framework;
using Moq;
using PlayersManagerLib;
using System;

namespace PlayerManager.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        private Mock<IPlayerMapper> _mockPlayerMapper;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mockPlayerMapper = new Mock<IPlayerMapper>();
        }

        [Test]
        public void RegisterNewPlayer_ShouldCreatePlayer_WhenNameIsValid()
        {
            _mockPlayerMapper.Setup(m => m.IsPlayerNameExistsInDb(It.IsAny<string>())).Returns(false);

            var player = Player.RegisterNewPlayer("Rohit", _mockPlayerMapper.Object);

            Assert.That(player, Is.Not.Null);
            Assert.That(player.Name, Is.EqualTo("Rohit"));
            Assert.That(player.Country, Is.EqualTo("India"));
            Assert.That(player.NoOfMatches, Is.EqualTo(30));
        }

        [Test]
        public void RegisterNewPlayer_ThrowsException_WhenNameIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => Player.RegisterNewPlayer("", _mockPlayerMapper.Object));
        }

        [Test]
        public void RegisterNewPlayer_ThrowsException_WhenNameAlreadyExists()
        {
            _mockPlayerMapper.Setup(m => m.IsPlayerNameExistsInDb("Rohit")).Returns(true);

            Assert.Throws<ArgumentException>(() => Player.RegisterNewPlayer("Rohit", _mockPlayerMapper.Object));
        }
    }
}
