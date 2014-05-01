# Cryptopay API C&#35; wrapper
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
##Balance
To get balance:
```csharp
Balance balance = cryptopay.RequestBalance();
Decimal balanceGBP = balance.Currency(Currency.GBP);
Decimal balanceEUR = balance.Currency(Currency.EUR);
Decimal balanceBTC = balance.Currency(Currency.BTC);
```

Check documantation for balance in BTC.

##Invoice
To create an invoice:

```csharp
Invoice invoice = cryptopay.RequestInvoice(
    10.1m, 
    Currency.GBP, 
    "C# Test", 
    "1234567890",
    "https://cryptopay.me",
    null,
    6,
    new Dictionary<string, string> { { "custom1", "November, 2013" }, { "custom2", "e-issue" } }
);
```
invoice object fields can be referenced by the following way
```csharp
invoice.Uuid
invoice.Description
invoice.Status
invoice.BtcPrice
invoice.BtcAddress
invoice.ShortId
invoice.CallbackParams["custom1"]
invoice.CallbackParams["custom2"]
invoice.Id
invoice.Price
invoice.Currency
invoice.CreatedAtTimestamp
invoice.CreatedAt
invoice.ValidTillTimestamp
invoice.ValidTill
invoice.BitcoinUri
invoice.CallbackUrl
invoice.SuccessRedirectUrl
```

CreatedAt and ValidTill fields allow user to get time as ```csharp DateTime ```

To get information about invoice:
```csharp
Invoice invoiceDuplicate = cryptopay.RequestInvoiceInfo("your invoice uuid");
```

To requote invoice:
```csharp
Invoice invoiceReqoute = cryptopay.RequoteInvoice("your invoice uuid");
```

To get all invoices:
```csharp
InvoiceCollection invoiceCollection = cryptopay.ListInvoices(<page>, <per_page>, <DateTime from>, <DateTime to>);
```

All parameters are optional. To use only <from> and <to> parameters set <page> and <per_page> to -1.
invoiceCollection object fields can be referenced by the following way
```csharp
invoiceCollection.Total
invoiceCollection.Page
invoiceCollection.TotalPages
invoiceCollection.Invoices
```
```csharp invoiceCollection.Invoices``` is a List of Invoices.

##Button
To create button:

```csharp
Button button = cryptopay.CreateButton(10.1m, Currency.GBP, "Button Test");
```
button object fields can be referenced by the following way
```csharp
button.Token
button.Name
button.Price
button.Currency
button.CreatedAtTimestamp
button.CreatedAt
button.CallbackUrl
```

##Hosted pages
To create hosted page:

```csharp
HostedPageItem hostedPageItem = new HostedPageItem();
hostedPageItem.Name = "Item";
hostedPageItem.Price = 11;
hostedPageItem.Quantity = 1;
hostedPageItem.Description = "";
hostedPageItem.VatRate = 0;

List<HostedPageItem> hostedPageItems = new List<HostedPageItem>();
hostedPageItems.Add(hostedPageItem);

HostedPageInvoice hostedPageInvoice = cryptopay.CreateHostedPage(hostedPageItems, Currency.EUR, "test");
```
hosted page object fields can be referenced by the following way
```csharp
hostedPageInvoice.Token
hostedPageInvoice.SuccessRedirectUrl
hostedPageInvoice.CallbackUrl
hostedPageInvoice.CollectEmail
hostedPageInvoice.CollectName
hostedPageInvoice.CollectAddress
hostedPageInvoice.CollectPhone
hostedPageInvoice.ChangeQuantity
hostedPageInvoice.Id
hostedPageInvoice.Price
hostedPageInvoice.Currency
hostedPageInvoice.CreatedAtTimestamp
hostedPageInvoice.CreatedAt
hostedPageInvoice.Url
hostedPageInvoice.Items
```

```csharp hostedPageInvoice.Items``` is a List of Items.

##Validate hash
To get validation hash (documentation sample below):
```csharp
hostedPageInvoice.Token
Cryptopay cryptopay = new Cryptopay("76b7c5d75bececcef0b44f01275d1357");
string hash = cryptopay.ValidationHash("248e5bb8-486c-457b-a2a3-59474baded6e", 10.0m, Currency.GBP);
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

#Dependencies
Current realisation depends on [Json.NET](http://james.newtonking.com/json) project 
