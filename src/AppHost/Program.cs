var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin();

var db = postgres.AddDatabase("fstimedb");

var migrationService = builder.AddProject<Projects.FSTime_Services_DatabaseMigration>("MigrationService")
    .WithReference(db);

var api = builder.AddProject<Projects.FSTime_Api>("Api")
    .WithReference(db);

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
