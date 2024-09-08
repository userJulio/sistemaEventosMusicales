using AutoMapper;
using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using EventosMusicales.Entities;
using EventosMusicales.Repositories;
using EventosMusicales.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Services.Implemetation
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository ventaRepository;
        private readonly IMapper mapper;
        private readonly IConciertoRepository conciertoRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ILogger<VentaService> logger;

        public VentaService(IVentaRepository ventaRepository,
            IMapper mapper,
            IConciertoRepository conciertoRepository,
            ICustomerRepository customerRepository,
            ILogger<VentaService> logger)
        {
            this.ventaRepository = ventaRepository;
            this.logger = logger;   
            this.mapper = mapper;
            this.conciertoRepository = conciertoRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<BaseResponseGeneric<int>> AddVentaAsync(string email, VentaRequestDto requestDto)
        {
           var response= new BaseResponseGeneric<int>();
            try
            {
                await ventaRepository.CreateTransactionAsing();
                var entityVenta = mapper.Map<Venta>(requestDto);
                var customer = await customerRepository.GetByEmailAsync(email);
                var nuevoCliente = new Customer();
                if (customer is null)
                {
                    //logica de negocios: Si no existe el cliente se crea uno nuevo 
                    
                    nuevoCliente.Email = requestDto.Email;
                    nuevoCliente.FullName=requestDto.FullName;
                    nuevoCliente.Id = await customerRepository.AddAsync(nuevoCliente);

                }
                entityVenta.CustomerId = customer is null ? nuevoCliente.Id: customer.Id;
                var concierto = await conciertoRepository.GetAsync(requestDto.ConciertoId);
                if(concierto is null)
                {
                    throw new Exception($"El concierto con el ID : {requestDto.ConciertoId} no existe");
                }
                if(DateTime.Today > concierto.DateEvent)
                {
                    throw new InvalidOperationException($"No se puede comprar tickets  para el concierto {concierto.Title} porque " +
                        $"ya paso");

                }
                if (concierto.Finalized)
                {
                    throw new Exception($"El concierto con el id : {concierto.Id} ya finalizo.");
                }
                entityVenta.Total = entityVenta.Quantity * concierto.UnitPrice;

                await ventaRepository.AddAsync(entityVenta);
                await ventaRepository.UpdateAsync();  // Se confirma la transaccion  y se guarda

                response.data = entityVenta.Id;
                response.Succes = true;
                logger.LogInformation("Se creo la venta correctamente para el email : {email}", email);

            }
            catch (InvalidOperationException ex)
            {
                await ventaRepository.RollBackAsync(); // Revierte la venta
                response.ErrorMessage = ex.Message;
                logger.LogWarning($"{response.ErrorMessage}");

            }
            catch (Exception ex)
            {
                await ventaRepository.RollBackAsync(); // Revierte la venta
                response.ErrorMessage = "Error al crear la venta";
                logger.LogError($" {response.ErrorMessage} {ex.Message}");

            }
            
            return response;
        }

        public async Task<BaseResponseGeneric<VentaResponseDto>> GetAsync(int id)
        {
            var response=new BaseResponseGeneric<VentaResponseDto>();
            try
            {
                var venta = await ventaRepository.GetAsync(id);
                response.data=mapper.Map<VentaResponseDto>(venta);
                response.Succes = response.data is not null ? true: false;


            }catch(Exception ex)
            {
                response.ErrorMessage = "Error al seleccionar la venta";
                logger.LogError($" {response.ErrorMessage} {ex.Message}");

            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<VentaResponseDto>>> Getasync(VentaByDateSearchDto search, PaginationDto paginationDto)
        {
            var response= new BaseResponseGeneric<ICollection<VentaResponseDto>>();
            try
            {
                var dateInit = Convert.ToDateTime(search.DateStart);
                var dateEnd = Convert.ToDateTime(search.DateEnd);

                var data = await ventaRepository.GetAsync(
                    predicate: x=>x.FechaVenta >= dateInit && x.FechaVenta <= dateEnd,
                    orderby: x=>x.NumeroOperacion,
                    paginationDto
                    );
                response.data= mapper.Map<ICollection<VentaResponseDto>>(data);
                response.Succes = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al filtrar las ventas por fecha";
                logger.LogError($" {response.ErrorMessage}  {ex.Message}");
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<VentaResponseDto>>> Getasync(string email, string? title, PaginationDto paginationDto)
        {
            var response= new BaseResponseGeneric<ICollection<VentaResponseDto>>();
            try
            {
                var data = await ventaRepository.GetAsync(
                    predicate:s=>s.customer.Email == email && s.concierto.Title.Contains(title ?? string.Empty),
                    orderby:x=>x.FechaVenta,
                    paginationDto
                    );
                response.data = mapper.Map<ICollection<VentaResponseDto>>(data);
                response.Succes = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al filtrar la ventas del usaurio por titulo";
                logger.LogError($"{response.ErrorMessage}  {ex.Message}");
            }
            return response;
        }
    }
}
