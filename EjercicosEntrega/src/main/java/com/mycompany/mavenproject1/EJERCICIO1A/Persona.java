/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */

package com.mycompany.mavenproject1.EJERCICIO1A;

/**
 *
 * @author Raul Gijon Sanchez - Maroto
 * Created on 16 sept 2025
 */
public class Persona {
    
    private int edad;
    
    public Persona(int edad){
        
        this.edad = edad;
    }
    
    public int getEdad(){
        return edad;
    }
    
    public void setEdad(int edad){
        this.edad = edad;
    }
    
    public static int randomEdad(){
        int min = 5;
        int max = 60;
        int num = (int)(Math.random() * (max - min + 1)) + min;
        return num;
    }
    
    public static int numPersonas(){
        int min = 0;
        int max = 50;
        int num = (int)(Math.random() * (max - min + 1)) + min;
        System.out.println("Número de personas a añadir: " + num);
        return num;
    }

}
