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
    public class ConciertoService : IConciertoService
    {
        private readonly IConciertoRepository repositoryConcierto;
        private readonly ILogger<ConciertoService> logger;
        private readonly IMapper mapper;
        private readonly IGeneroRepositorio generoRepositorio;

        public ConciertoService(IConciertoRepository repositoryConcierto,
            ILogger<ConciertoService> logger,
            IMapper mapper,
            IGeneroRepositorio generoRepositorio)
        {
            this.repositoryConcierto = repositoryConcierto;
            this.logger = logger;
            this.mapper = mapper;
            this.generoRepositorio = generoRepositorio;
        }
        public async Task<BaseResponseGeneric<ICollection<ConciertoResponseDto>>> GetAsync(string? title)
        {
            var response = new BaseResponseGeneric<ICollection<ConciertoResponseDto>>();
            try
            {
                var data= await repositoryConcierto.getDataConcert(title?? string.Empty);

                response.data=mapper.Map<ICollection<ConciertoResponseDto>>(data);
                response.Succes = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError($"{response.ErrorMessage} - {ex.Message} ");
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ConciertoResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<ConciertoResponseDto>();
            try
            {
                var data = await repositoryConcierto.GetAsync(id);
                if(data is null)
                {
                    response.ErrorMessage = $"No se encontro el concierto con id : {id}";
                    logger.LogWarning(response.ErrorMessage);
                    return response;
                }

                response.data = mapper.Map<ConciertoResponseDto>(data);
                response.Succes = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError($"{response.ErrorMessage} - {ex.Message} ");
            }
            return response;
        }

        public async Task<BaseResponseGeneric<int>> Addasync(ConciertoRequestDto requestDto)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var genero = await generoRepositorio.GetAsync(requestDto.GenreId);
                if(genero is null)
                {
                    response.ErrorMessage = $"No se encontro el genero con id : {requestDto.GenreId}, no se puede guardar el concierto";
                    logger.LogWarning(response.ErrorMessage);
                    return response;
                }

                var itemConcierto = mapper.Map<Concierto>(requestDto);
                var data = await repositoryConcierto.AddAsync(itemConcierto);
                response.data = data;
                response.Succes = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al agregar los datos";
                logger.LogError($"{response.ErrorMessage} - {ex.Message} ");
            }
            return response;
        }
        public async Task<BaseResponse> Update(int id, ConciertoRequestDto requestDto)
        {
            var response = new BaseResponse();
            try
            {
                var item = await repositoryConcierto.GetAsync(id);
                if(item is not null)
                {

                    //mapping
                    //var requestmapping = mapper.Map<Concierto>(requestDto);
                    // item = requestmapping;
                    mapper.Map(requestDto,item);  // el requestDto lo pega en el item
                    await repositoryConcierto.UpdateAsync();
                    response.Succes = true;
                }
                else
                {
                    response.ErrorMessage = $"El registro con id : {id} no encontrado";
                    return response;
                }

            

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al actualizar los datos";
                logger.LogError($"{response.ErrorMessage} - {ex.Message} ");
            }
            return response;
        }
        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var item = await repositoryConcierto.GetAsync(id);
                if (item is  null)
                {
                    response.ErrorMessage = $"El registro con id : {id} no encontrado";
                    return response;
                }
                await repositoryConcierto.DeleteAsync(id);
                response.Succes = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al actualizar los datos";
                logger.LogError($"{response.ErrorMessage} - {ex.Message} ");
            }
            return response;
        }

        public async Task<BaseResponse> FinalizeAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var item = await repositoryConcierto.GetAsync(id);
                if(item is null)
                {
                    response.ErrorMessage = $"El registro con id : {id} no encontrado";
                    return response;
                }
           
                await repositoryConcierto.FinalizeAsync(id);
                response.Succes = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al actualizar el finalize";
                logger.LogError($"{response.ErrorMessage} - {ex.Message} ");
            }
            return response;
        }




    }
}
