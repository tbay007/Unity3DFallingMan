using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class GameSettingsModel
    {
        public int Id { get; set; }
        public decimal Volume { get; set; }
        public float platformSpeed { get; set; }

        public float spawnSpeed { get; set; }

        public List<HighScoreModel> HighScores { get; set; }

        public bool Exploded { get; set; }
        public string DateEntered { get; set; }
    }
}
