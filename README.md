# XAF Development Essentials 2025

Welcome to this guide highlighting key features and concepts for developers starting with DevExpress eXpressAppFramework (XAF) in 2025. This repository serves as a practical reference rather than a step-by-step tutorial, focusing on modern development patterns and important architectural decisions.

## Purpose

This repository demonstrates:
- Modern XAF development practices
- Essential architectural patterns
- Cross-platform development strategies


## Who Is This For?

- Developers new to XAF in 2025
- Teams transitioning to modern XAF architecture
- Developers looking to understand some of best practices
- Anyone interested in enterprise application development with XAF

## Starting with XAF

### Creating your first project 

### Understanding the project layout

### Creating Controllers

### ViewControllers vs WindowControllers 

### Interacting with Views, Frames, and ObjectSpaces

### Creating Actions

### Model and Modules

## New Features in Version 22.1

[Reference Documentation](https://www.devexpress.com/subscriptions/new-2022-1.xml#xaf)

### Unified Application Configuration for WinForms & Blazor (.NET 6)

Services can now be configured in a unified manner:

```csharp
services.AddXaf(Configuration, builder => {
    builder.UseApplication<MainDemoBlazorApplication>();
    builder.Modules
        .Add<MainDemoModule>()
        .Add<MainDemoBlazorApplicationModule>()
        .AddAuditTrail(options =>
            options.AuditDataItemPersistentType = typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent))
        .AddFileAttachments()
        .AddObjectCloning()
        .AddOffice()
        .AddReports()
        .AddValidation();
});
```

### XAF Solution Structure - Simplified for .NET 6

The solution structure has been simplified by removing `SolutionName.Module.XXX` projects. Controllers, business classes, list and property editors, view items, and other XAF-specific entities can now be added directly to `SolutionName.XXX` application projects. This simplification helps new XAF developers get started more easily and avoid common mistakes.

### End-to-End Testing

End-to-end (e2e/functional) tests can now be written in C#/VB.NET. 

[Documentation](https://docs.devexpress.com/eXpressAppFramework/403852/debugging-testing-and-error-handling/functional-tests-easy-test?v=22.1)

Example test:
```csharp
[Theory]
[InlineData(BlazorAppName)]
[InlineData(WinAppName)]
public void CreateStudent(string applicationName) {
    FixtureContext.DropDB(AppDBName);
    var appContext = FixtureContext.CreateApplicationContext(applicationName);
    appContext.RunApplication();
    appContext.GetForm().FillForm(("User Name", "Admin"));
    appContext.GetAction("Log In").Execute();

    appContext.Navigate("Student");
    Assert.Equal(0, appContext.GetGrid().GetRowCount());

    appContext.GetAction("New").Execute();
    appContext.GetForm().FillForm(("First Name", "John"), ("Last Name", "Smith"));
    appContext.GetAction("Save").Execute();
    Assert.Equal("John Smith", appContext.GetForm().GetPropertyValue("Full Name"));

    appContext.Navigate("Student");
    Assert.Equal(1, appContext.GetGrid().GetRowCount());
    Assert.Equal(new string[] { "John", "Smith" }, appContext.GetGrid().GetRow(0, "First Name", "Last Name"));
}
```

### New Error Diagnostics

XAF now includes built-in error diagnostics viewable in the Error List window. [Documentation](https://docs.devexpress.com/eXpressAppFramework/403389/debugging-testing-and-error-handling/code-diagnostics?v=22.1)

New diagnostics include:

- XAF0009: Properties with `RuleRequiredFieldAttribute` must be nullable or reference type
- XAF0010: Set `DelayedAttribute.UpdateModifiedOnly` to True
- XAF0011: Implement delayed property correctly
- XAF0012: Avoid `XafApplication.CreateObjectSpace()` without Type parameter
- XAF0013: Avoid reading `XafApplication.ConnectionString`
- XAF0014: Property with Association attribute must have correct unique pair
- XAF0015: Association must not have Aggregated attribute if paired to 'many' end
- XAF0016: NonPersistent objects must have DomainComponent attribute

## Dependency Injection in XAF

### IPlatformInfo Interface

The `IPlatformInfo` interface provides platform-specific information:

```csharp
public interface IPlatformInfo
{
    string GetPlatformName();
}
```

### Implementation in DiInBo.cs

```csharp
public class DiInBo : BaseObject
{
    public DiInBo(Session session) : base(session) { }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
        this.PlatformName = Session.ServiceProvider.GetRequiredService<IPlatformInfo>().GetPlatformName();
    }

    string platformName;

    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string PlatformName
    {
        get => platformName;
        set => SetPropertyValue(nameof(PlatformName), ref platformName, value);
    }
}
```

### Usage in DiController.cs

```csharp
public class DiController : ViewController
{
    SimpleAction ShowPlatformInfoFromApplication;
    SimpleAction ShowPlatformInfo;

    private readonly IServiceProvider serviceProvider;

    public DiController() : base()
    {
        ShowPlatformInfo = new SimpleAction(this, "Show Platform Info", "View");
        ShowPlatformInfo.Execute += ShowPlatformInfo_Execute;

        ShowPlatformInfoFromApplication = new SimpleAction(this, "ShowPlatformInfoFromApplication", "View");
        ShowPlatformInfoFromApplication.Execute += ShowPlatformInfoFromApplication_Execute;
    }

    [ActivatorUtilitiesConstructor]
    public DiController(IServiceProvider serviceProvider) : this()
    {
        this.serviceProvider = serviceProvider;
    }

    private void ShowPlatformInfo_Execute(object sender, SimpleActionExecuteEventArgs e)
    {
        IPlatformInfo platformInfo = serviceProvider.GetService<IPlatformInfo>();
        ShowMessage(platformInfo.GetPlatformName());
    }

    private void ShowPlatformInfoFromApplication_Execute(object sender, SimpleActionExecuteEventArgs e)
    {
        IPlatformInfo platformInfo = this.Application.ServiceProvider.GetService<IPlatformInfo>();
        ShowMessage(platformInfo.GetPlatformName());
    }

    private void ShowMessage(string message)
    {
        MessageOptions options = new MessageOptions
        {
            Duration = 3000,
            Message = message,
            Type = InformationType.Success,
            Web = { Position = InformationPosition.Top },
            Win = { Caption = "Platform Info", Type = WinMessageType.Flyout }
        };
        Application.ShowViewStrategy.ShowMessage(options);
    }
}
```

### Platform-Specific Registration

#### Blazor Server (XafGettingStarted2025.Blazor.Server\Startup.cs)

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Other service registrations...
    services.AddScoped<IPlatformInfo, PlatformInfoBlazor>();
}
```

#### Windows Forms (XafGettingStarted2025.Win\Startup.cs)

```csharp
public static WinApplication BuildApplication(string connectionString)
{
    var builder = WinApplication.CreateBuilder();
    
    // Other configurations...
    builder.Services.AddScoped<IPlatformInfo, PlatformInfoWin>();

    var winApplication = builder.Build();
    return winApplication;
}
```

The `IPlatformInfo` interface and its implementations provide consistent platform-specific information across different parts of the application.

I'll help reformat this into a clearer Markdown structure.

# Central Package Management (CPM)

## Overview

XAF v24.2 Solution Wizard includes a Central Package Management (CPM) option. This feature:
- Creates a `Directory.Packages.props` file at the root of your repository
- Sets the MSBuild property `ManagePackageVersionsCentrally` to true

## Microsoft's Description

Microsoft emphasizes CPM's value as follows:

> Dependency management is a core feature of NuGet. Managing dependencies for a single project can be easy. Managing dependencies for multi-project solutions can prove to be difficult as they start to scale in size and complexity. In situations where you manage common dependencies for many different projects, you can leverage NuGet's central package management (CPM) features to do all of this from the ease of a single location.

## Security Updates

The `Directory.Packages.props` file (including 3rd party dependencies) can be updated by the DevExpress Project Converter based on known GitHub security advisories (GitHub advisories).

# Control Field Visibility for Various UI Contexts

## New HideInUI Attribute

XAF v24.2 introduces a new `HideInUI` attribute for use within business classes. This declarative approach:
- Simplifies the process of hiding fields from ListView, DetailView, and various UI contexts
- Works with Filter, Report, and Dashboard Editor/Designer Field Lists
- Eliminates the need for manual Controller-based solutions
- Replaces multiple non-flexible `VisibleInXXX` and `Browsable` attributes

## Usage Example

```csharp
[HideInUI(HideInUI.All)]
public DateTime CreatedOn
{
    get => createdOn;
    set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
}
```

