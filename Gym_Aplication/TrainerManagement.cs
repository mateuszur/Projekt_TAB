﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace Gym_Aplication
{
    internal class TrainerManagement
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string E_Mail { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string DateOfEmployment { get; set; }

        public List<TrainerManagement> trainerList = new List<TrainerManagement>();
    }
}