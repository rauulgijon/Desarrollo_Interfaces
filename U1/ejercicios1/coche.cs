using System;

namespace Ejercicios1
{
	internal class Coche
	{
		private int id;
		private string marca;
		private string modelo;
		private int km;
		private double precio;

		public Coche(int id, string marca, string modelo, int km, double precio)
		{
			this.id = id;
			this.marca = marca;
			this.modelo = modelo;
			this.km = km;
			this.precio = precio;
		}
		public int GetId()
		{
			return id;
		}
		public string GetMarca()
		{
			return marca;
		}
		public string GetModelo()
		{
			return modelo;
		}
		public int GetKm()
		{
			return km;
		}
		public double GetPrecio()
		{
			return precio;
		}
		public void SetId(int id)
		{
			this.id = id;
		}
		public void SetMarca(string marca)
		{
			this.marca = marca;
		}
		public void SetModelo(string modelo)
		{
			this.modelo = modelo;
		}
		public void SetKm(int km)
		{
			this.km = km;
		}
		public void SetPrecio(double precio)
		{
			this.precio = precio;
		}
		public override string ToString()
		{
			return "ID: " + id + ", Marca: " + marca + ", Modelo: " + modelo + ", Km: " + km + ", Precio: " + precio;
		}
	}
}
