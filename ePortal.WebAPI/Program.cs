using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SQL Connection pgas_eportal_v2
builder.Services.AddDbContext<PGAS.WebAPI.Context.pgas_eportal_v2Context>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("portal_v2"));
    }
    );

//SQL Connection pmis
builder.Services.AddDbContext<PGAS.WebAPI.Context.pmisContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("db_pmis.pmis"));
    }
    );

//SQL Connection others
builder.Services.AddDbContext<PGAS.WebAPI.Context.othersContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("db_others.dts"));
    }
    );

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin  
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
