var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataBindMount(source: @"C:\tmp\fstimedb", isReadOnly: false)
    .WithPgAdmin();

var db = postgres.AddDatabase("fstimedb");

var api = builder.AddProject<Projects.FSTime_Api>("Api");

var client = builder.AddNpmApp("Client", "../Client", "dev")
    .WithNpmPackageInstallation()
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("ApiUrl", api.GetEndpoint("https"))
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();
;

builder.Build().Run();
