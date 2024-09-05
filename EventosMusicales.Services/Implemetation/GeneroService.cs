using AutoMapper;
using Castle.Core.Logging;
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
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepositorio generoRepositorio;
        private readonly ILogger<GeneroService> logger;
        private readonly IMapper mapper;

        public GeneroService(IGeneroRepositorio generoRepositorio, ILogger<GeneroService> logger,IMapper mapper)
        {
            this.generoRepositorio = generoRepositorio;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<BaseResponseGeneric<ICollection<GeneroResponseDto>>> GetAsync(string? name)
        {
            var response = new BaseResponseGeneric<ICollection<GeneroResponseDto>>();

            try
            {
                var generosdb= await generoRepositorio.GetGeneros(name);
                response.data = mapper.Map<ICollection<GeneroResponseDto>>(generosdb);
                response.Succes = true;

            }catch(Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos de Generos";
                logger.LogError($"{response.ErrorMessage}  {ex.Message}");

            }
            return response;

        }

        public async Task<BaseResponseGeneric<GeneroResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<GeneroResponseDto>();
            try
            {
                var generodb = await generoRepositorio.GetAsync(id); 
                if(generodb is null)
                {
                    response.ErrorMessage = $"No se encontro el Genero con id : {id}";
                    response.Succes = false;
                    logger.LogError(response.ErrorMessage);
                    return response;
                }
                response.data = mapper.Map<GeneroResponseDto>(generodb);
                response.Succes = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos de generos";
                logger.LogError($"{response.ErrorMessage}  {ex.Message}");
            }
            return response;

        }
        public async Task<BaseResponseGeneric<int>> Addasync(GeneroRequestDto requestDto)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var generodb = mapper.Map<Generos>(requestDto);
                var itemNuevo = await generoRepositorio.AddAsync(generodb);
                response.data = itemNuevo;
                response.Succes = true;

            }catch(Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al guardar el genero";
                logger.LogError($"{response.ErrorMessage} - {ex.Message}");
            }
            return response;
        }

        public async Task<BaseResponse> Update(int id, GeneroRequestDto requestDto)
        {
            var response = new BaseResponse();
            try
            {
                var item = await generoRepositorio.GetAsync(id);
                if(item is null)
                {
                    response.ErrorMessage = $"No se encontro el Genero con id :{id}";
                    logger.LogError(response.ErrorMessage);
                    return response;
                }
                mapper.Map(requestDto, item);
                await generoRepositorio.UpdateAsync();
                response.Succes = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"Ocurrio un error al actualizar el registro de genero con id : {id}";
                logger.LogError($"{response.ErrorMessage}  {ex.Message}");

            }
            return response;
        }


        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var item = await generoRepositorio.GetAsync(id);
                if(item is null)
                {
                    response.ErrorMessage = $"No se encontro el genero con id :  {id}";
                    response.Succes = false;
                    return response;
                }

                await generoRepositorio.DeleteAsync(id);
                response.Succes = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al eliminar el genero";
                logger.LogError($" {response.ErrorMessage}  {ex.Message}");
            }

            return response;
        }
    }
}
