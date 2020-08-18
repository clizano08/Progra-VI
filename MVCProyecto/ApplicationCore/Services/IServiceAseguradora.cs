﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
 public interface IServiceAseguradora
    {
        IEnumerable<Aseguradora> GetAseguradora();
        Aseguradora GetAseguradoraByID(int id);
        void DeleteAseguradora(int id);
        Aseguradora Save(Aseguradora aseguradora);
    }
}