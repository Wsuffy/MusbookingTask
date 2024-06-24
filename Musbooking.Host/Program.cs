using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Musbooking.Application.Models.Requests.Equipment;
using Musbooking.Application.Models.Requests.Order;
using Musbooking.Application.Requests.Validators;
using Musbooking.Dal.Abstractions;
using Musbooking.Dal.Implementation.Implementation;
using Musbooking.Host.Filters;
using Musbooking.Service.Abstractions.Abstractions;
using Musbooking.Service.Implementation.Commands.Equipment;
using Musbooking.Service.Implementation.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Contexts

builder.Services.AddDbContext<IOrderReadContext, OrderReadContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDbContext<IOrderWriteContext, OrderWriteContext>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
builder.Services.AddScoped<IOrderEquipmentRepository, OrderEquipmentRepository>();

#endregion

#region Utils

builder.Services.AddControllers(options => { options.Filters.Add<ExceptionFilter>(0); });


builder.Services.AddScoped<IValidator<EquipmentRequest>, CreateEquipmentValidator>();
builder.Services.AddScoped<IValidator<OrderRequest>, CreateOrderValidator>();
builder.Services.AddFluentValidationAutoValidation();

#endregion

#region CQRS

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<AddEquipmentCommand>());

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.UseHttpsRedirection();

app.Run();