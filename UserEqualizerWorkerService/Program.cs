using FCBankBasicHelper.Models;
using UserEqualizerWorkerService;
using UserEqualizerWorkerService.Data;
using UserEqualizerWorkerService.Services.v1;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<FcbankBasicContext>();
        services.AddDbContext<FcbankBasicContext>();
        services.AddTransient<UserEqualizerService>();
        services.AddHttpClient<PlaceHolderClient>(client =>
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        });

    })
    .Build();

await host.RunAsync();
