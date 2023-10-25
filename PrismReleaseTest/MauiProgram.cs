using Microsoft.Extensions.Logging;
using PrismReleaseTest.ViewModels;

namespace PrismReleaseTest;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UsePrism(prismBuilder =>
                prismBuilder.RegisterTypes(containerRegistry =>
                {
                    containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
                })
                .OnAppStart((containerProvider, navigationService) =>
                    navigationService.CreateBuilder()
                                     .AddNavigationPage()
                                     .AddSegment<MainViewModel>()
                                     .NavigateAsync(OutputExceptionToConsole)
                )
			)
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
    }

    private static void OutputExceptionToConsole(Exception ex)
    {
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");

        Console.WriteLine($"Exception occurred!{Environment.NewLine}  Message: {ex.Message}{Environment.NewLine}  Stacktrace: {ex.StackTrace}");

        while (ex?.InnerException != null)
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            ex = ex.InnerException;

            Console.WriteLine($"Inner Exception:{Environment.NewLine}  Message: {ex.Message}{Environment.NewLine}  Stacktrace: {ex.StackTrace}");
        }

        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");
    }

}

