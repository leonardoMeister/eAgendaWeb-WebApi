﻿using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.wepApi.ViewModels.DespesaModels;
using System;

namespace eAgenda.wepApi.Controllers.Config.AutoMapperConfig
{
    public class DespesaProfile:Profile
    {
        public DespesaProfile() 
        {
            ConverterDeEntidadeParaViewMovel();

            ConverterDeViewModelParaEntidade();
        }

        private void ConverterDeViewModelParaEntidade()
        {

        }

        private void ConverterDeEntidadeParaViewMovel()
        {
            CreateMap<Despesa, ListarDespesaViewModels>();
        }
    }
}
