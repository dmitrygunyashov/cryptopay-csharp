# Cryptopay
Easily accept Bitcoin payment with Cryptopay Payment API

Before you will be able to start using this gem, you will need to obtain an API key in Account - Merchant Tools - Settings.


# Installation
##To add a reference in Visual C&#35;
1. In Solution Explorer, right-click the project node and click Add Reference.
2. In the Add Reference dialog box, select the tab indicating the type of component you want to reference.
3. Select the components you want to reference (CryptopayApi.dll), and then click OK.

And set your api key.

```csharp
Cryptopay cryptopay = new Cryptopay("your secret api-key");
```

# Basic Usage

To create an invoice:

```csharp
Invoice invoice = cryptopay.RequestInvoice(
    10,
    Currency.GBP,
    "C# Test",
    "1234567890",
    new Dictionary<string, string> { { "custom1", "November, 2013" }, { "custom2", "e-issue" } },
    null,
    null,
    null
);
```
invoice object fields can be referenced by the following way
```csharp
invoice.Uuid;
invoice.Description;
invoice.Status;
invoice.BtcPrice;
invoice.BtcAddress;
invoice.ShortId;
invoice.CallbackParams["custom1"];
invoice.CallbackParams["custom2"];
invoice.Id;
invoice.Price;
invoice.Currency;
invoice.CreatedAt;
invoice.ValidTill;
```

# Errors handle
Exception handling should be done by user:
```csharp
try
{
    Invoice invoice = cryptopay.RequestInvoice(
        10,
        Currency.GBP,
        "C# Test",
        "1234567890",
        new Dictionary<string, string> { { "custom1", "November, 2013" }, { "custom2", "e-issue" } },
        null,
        null,
        null
    );
}
catch (Exception e) 
{
    System.Diagnostics.Debug.WriteLine(e.Message);
    System.Diagnostics.Debug.WriteLine(e.StackTrace);
}

```
