using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ReservaHotel.Data.Database;

public class Quarto : BaseEntity
{

    public int Numero { get; set; }

    public float DiariaDolar { get; set; }

    public string Ocupacao { get; set; }

    public Guid HotelId { get; set; }

    public TipoQuarto TipoQuarto { get; set; }


    public int Andar { get; set; }

    public float Tamanho { get; set; }

    public DateTime? UltimaAtualizacaoPreco { get; set; }
    public float DiariaReal { get; set; }

    public Hotel Hotel { get; set; }
}
