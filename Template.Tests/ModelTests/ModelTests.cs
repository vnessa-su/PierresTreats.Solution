using Microsoft.VisualStudio.TestTools.UnitTesting;
using Template.Models;

namespace Template.Tests
{
  [TestClass]
  public class ModelTests
  {
    public ModelTests()
    {
      DBConfiguration.ConnectionString = "Server=localhost;Port=[port number];Database=[test database];Uid=[username];Pwd=[password];";
    }

    [TestInitialize]
    public void Initialize()
    {
      //run before each test
    }

    [TestCleanup]
    public void Cleanup()
    {
      //code to run after each test
    }

    [TestMethod]
    public void MethodToTest_Input_ExpectedOutput()
    {
      //arrange, act, assert
    }
  }
}