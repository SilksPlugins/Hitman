﻿using Hitman.API.Hits;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hitman.Database.Models
{
    public class HitData : IHitData
    {
        [Key]
        public int HitId { get; set; }

        [Required]
        public string TargetPlayerId { get; set; }

        public string? HirerPlayerId { get; set; }

        [Required]
        public decimal Bounty { get; set; }

        [Required]
        public DateTime TimePlaced { get; set; }

        public HitData()
        {
            HitId = 0;
            TargetPlayerId = "";
            HirerPlayerId = null;
            Bounty = 0;
            TimePlaced = default;
        }
    }
}