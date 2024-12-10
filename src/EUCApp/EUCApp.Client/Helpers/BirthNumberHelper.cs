namespace EUCApp.Client.Helpers;

public class BirthNumberHelper
{
  private string? _errorCode;
  private string? _value;
  private int? _year;
  private int? _month;
  private int? _day;
  private int? _code;
  private bool? _isMale;
  private DateTime? _birthDate;

  public string? ErrorCode => _errorCode;

  public bool IsValid => _errorCode == null || _value == null;
  public bool? IsMale => _isMale;

  public DateTime? BirthDate => _birthDate;

  public string? Value
  {
    get => _value;
    set
    {
      _value = value;
      Init();
    }
  }

  public BirthNumberHelper(string? number)
  {
    _value = number;
    Init();
  }

  private void Init()
  {
    _isMale = null;
    _year = null;
    _month = null;
    _day = null;
    _code = null;
    _birthDate = null;

    if (_value == null)
      return;

    _value = _value.Trim();
    _value = _value.Replace("/", string.Empty);

    if (_value.Length != 9 && _value.Length != 10)
    {
      _errorCode = "ERR1";
      return;
    }

    _year = OnlyNumber(_value.Substring(0, 2));
    var yearOriginal = _year;
    if (_year == null)
    {
      _errorCode = "ERR2";
      return;
    }

    _month = OnlyNumber(_value.Substring(2, 2));
    if (_month == null)
    {
      _errorCode = "ERR3";
      return;
    }

    var mothOriginal = _month.Value;
    _isMale = true;
    if (_month > 50)
    {
      _isMale = false;
      _month -= 50;
    }

    if (_month < 1 || _month > 12)
    {
      _errorCode = "ERR3";
      return;
    }


    _day = OnlyNumber(_value.Substring(4, 2));
    if (_day == null || _day < 1 || _day > 31)
    {
      _errorCode = "ERR4";
      return;
    }

    _code = OnlyNumber(_value.Substring(6));
    if (_code == null)
    {
      _errorCode = "ERR5";
      return;
    }

    if (yearOriginal < 54 && _code < 1000)
      _year = 1900 + yearOriginal;
    else
      switch (yearOriginal)
      {
        case >= 54 and < 100:
          _year = 1900 + yearOriginal;
          break;
        default:
          _year = 2000 + yearOriginal;
          break;
      }

    if (_year > 1953)
    {
      var code1 = OnlyNumber(_value.Substring(6, 2)) ?? 0;
      var code2 = OnlyNumber(_value.Substring(8)) ?? 0;

      var sum = yearOriginal + mothOriginal + _day + code1 + code2;
      if (sum % 11 != 0)
      {
        _errorCode = "ERR6";
        return;
      }
    }

    try
    {
      if (_year != null)
        _birthDate = new DateTime(_year.Value, _month.Value, _day.Value);
      else
        _errorCode = "ERR7";
    }
    catch
    {
      _errorCode = "ERR7";
      return;
    }

    _errorCode = null;
  }


  private static int? OnlyNumber(string number)
  {
    if (!number.All(char.IsDigit))
      return null;

    if (int.TryParse(number, out var num))
      return num;

    return null;
  }
}