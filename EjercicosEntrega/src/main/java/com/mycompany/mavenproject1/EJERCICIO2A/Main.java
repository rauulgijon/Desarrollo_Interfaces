/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */

package com.mycompany.mavenproject1.EJERCICIO2A;

import com.mycompany.mavenproject1.ListaEnlazada;

/**
 *
 * @author Raul Gijon Sanchez - Maroto
 * Created on 16 sept 2025
 */
public class Main {
    
    public static void main(String []args){
        
        ListaEnlazada <Producto> cola = new ListaEnlazada<>();
        
        // Creo los productos de forma aleatoria
        int numProductos = Producto.randomCantidad();
        
        for (int i = 1; i < numProductos ; i++){
            int cantidad = Producto.randomCantidad();
            double precio = Producto.randomPrecio();
            Producto p = new Producto(cantidad, precio);
            cola.insertarUltimo(p);
        }
        
        System.out.println("Producto\tCantidad\tPrecio\tTotal");
        System.out.println("----------------------------------------------");
        
        
        double totalFinal = 0.0;
        
        for (int i = 0; i < cola.cuantosElementos(); i ++){
            
            Producto p = cola.devolverDato(i);
            double total = p.precioFinal();
            totalFinal += total;
            
            System.out.println("Producto " + (i + 1) + "\t"
                    + p.getCantidad() + "\t\t" + 
                    p.getPrecio() + "€\t" +
                    total + "€");
            
        }
        
        System.out.println("----------------------------------------------");
        System.out.println("TOTAL FINAL: " + totalFinal + "€");
        
        
        
    }

}
