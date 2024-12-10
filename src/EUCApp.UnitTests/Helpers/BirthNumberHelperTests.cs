using EUCApp.Client.Helpers;
using FluentAssertions;

namespace EUCApp.UnitTests.Helpers;

public class BirthNumberHelperTests
{
  [Theory, MemberData(nameof(ValidCases))]
  public void BirthNumberValidTest(string birthNumber, bool isMale, DateTime birthDate)
  {
    var helperAsSut = new BirthNumberHelper(birthNumber);
    helperAsSut.IsValid.Should().BeTrue();
    helperAsSut.ErrorCode.Should().BeNull();
    helperAsSut.IsMale.Should().Be(isMale);
    helperAsSut.BirthDate.Should().Be(birthDate);
  }

  [Theory, MemberData(nameof(InValidCases))]
  public void BirthNumberInValidTest(string birthNumber, string errorMessage)
  {
    var helperAsSut = new BirthNumberHelper(birthNumber);
    helperAsSut.IsValid.Should().BeFalse();
    helperAsSut.ErrorCode.Should().Be(errorMessage);
  }

  public static TheoryData<string, bool, DateTime> ValidCases =
    new()
    {
      { "536001/507", false, new DateTime(1953, 10, 1) },
      { "531001/454", true, new DateTime(1953, 10, 1) },
      { "530501/865", true, new DateTime(1953, 5, 1) },
      { "535501/002", false, new DateTime(1953, 5, 1) },
      { "856010/7787", false, new DateTime(1985, 10, 10) },
      { "851010/1182", true, new DateTime(1985, 10, 10) },
      { "011010/3455", true, new DateTime(2001, 10, 10) },
      { "016010/2888", false, new DateTime(2001, 10, 10) },
    };

  public static TheoryData<string, string> InValidCases =
    new()
    {
      { "a36001/507aa", "ERR1" },
      { "a36001/507", "ERR2" },
      { "13b001/507", "ERR3" },
      { "137001/507", "ERR3" },
      { "1310a1/507", "ERR4" },
      { "131091/507", "ERR4" },
      { "131011/a07", "ERR5" },
      { "016010/2889", "ERR6" },
    };
}