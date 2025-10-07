/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */

package com.mycompany.mavenproject1.EJERCICIO3;

/**
 *
 * @author Raul Gijon Sanchez - Maroto
 * Created on 12 sept 2025
 */
import java.util.Stack;

public class Main {
    public static void main(String[] args) {
        // Creamos la pila de informes
        Stack<Informe> pilaInformes = new Stack<>();

        // Agregamos 10 informes
        System.out.println("Agregando 10 informes...");
        for (int i = 1; i <= 10; i++) {
            int indiceTarea = i % 3; // alternamos entre administrativo, empresarial y personal
            pilaInformes.push(new Informe(i, indiceTarea));
        }

        // Mostramos el contenido de la pila
        System.out.println("Contenido de la pila (10 informes):");
        for (Informe inf : pilaInformes) {
            System.out.println(inf);
        }

        // Eliminamos todo el contenido
        System.out.println("Eliminando todos los informes...");
        while (!pilaInformes.isEmpty()) {
            System.out.println("Eliminado -> " + pilaInformes.pop());
        }

        // Agregamos 5 informes
        System.out.println("Agregando 5 informes...");
        for (int i = 11; i <= 15; i++) {
            int indiceTarea = i % 3;
            pilaInformes.push(new Informe(i, indiceTarea));
        }

        // Mostramos el contenido de la pila
        System.out.println("Contenido de la pila (5 informes):");
        for (Informe inf : pilaInformes) {
            System.out.println(inf);
        }
    }
}

