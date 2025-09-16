/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */

package com.mycompany.mavenproject1.EJERCICIO2A;

import java.text.DecimalFormat;

/**
 *
 * @author Raul Gijon Sanchez - Maroto
 * Created on 16 sept 2025
 */
public class Producto {
    
    private int cantidad;
    private double precio;
    
    public Producto(int cantidad, double precio){
        this.cantidad = cantidad;
        this.precio = precio;
        
    }
    
    public int getCantidad(){
        return cantidad;
    }
    
    public double getPrecio(){
        return precio;
    }
    
    public double precioFinal(){
        DecimalFormat df = new DecimalFormat("#,##");
        return Double.parseDouble(df.format(this.precio * this.cantidad));
    }
    
    public static int randomCantidad(){
        int max = 8;
        int min = 1;
        int num = (int)(Math.random() * (max - min + 1)) + min;
        return num;
    }
    
    public static double randomPrecio(){
        
        int min = 1;
        int max = 100;
        int num = (int)(Math.random() * (max - min + 1)) + min;
        return num;
        
    }

}
