/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */

package com.mycompany.mavenproject1.EJERCICIO1B;

import java.util.LinkedList;

/**
 *
 * @author Raul Gijon Sanchez - Maroto
 * Created on 16 sept 2025
 */
public class Main {

    public static void main(String []args){
        
    LinkedList<Persona> cola = new LinkedList<>();
    
    
    //Numero de personas en la cola
    int numPersonas = Persona.numPersonas();
    
    //Las añado en la lista con la edad aleatoria entre 5 y 60
    for (int i = 0; i < numPersonas; i++){
        int edad = Persona.randomEdad();
        cola.add(new Persona(edad));
    }
    
    //Ahora vamos con la recaudacion
    double total = 0.0;
    while (!cola.isEmpty()){
        Persona p = cola.removeFirst();
        int edad = p.getEdad();
        double precio = 0.0;
        
        if (edad >= 5 && edad <= 10){
            precio = 1.0;
        } else if (edad > 10 && edad <= 17){
            precio = 2.5;
        } else if (edad >= 18){
            precio = 3.5;
        }
        
        System.out.println("La persona con edad " + edad + " paga " + precio); 
        
        total += precio;
        
    }
    
    System.out.println("---------------------------------");
    System.out.println("Recaudación total: " + total + " €");
    System.out.println("Personas restantes en la cola: " + cola.size());
        
   
    
    }
    
}
