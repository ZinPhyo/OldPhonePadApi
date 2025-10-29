using OldPhonePad.Services;
using OldPhonePadApi.Helpers.DecodeInput;
using OldPhonePadApi.Helpers.ValidationInput;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IOldPhonePadService, OldPhonePadService>();
builder.Services.AddTransient<IValidator, Validator>();
builder.Services.AddTransient<IDecoder, Decoder>();

var app = builder.Build();

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
