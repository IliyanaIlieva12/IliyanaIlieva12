﻿namespace tattoo.Data
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Tattoo> Tattoos { get; set; }

    }
}
