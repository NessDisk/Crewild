using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class libreriaDeScrips : MonoBehaviour
{
    public Inventario inventario;
    public Items Itens;
    public CrewildChoiseSelect SeleccionDeCriaturas;
    public animationScritpBatle Batalla;
    public informacionCrewild informacionCrewild;
    public AllAttacks Ataques;
    public AllCrewild Crewilldslist;
    public AllPostura todasLasPosturas;
    public PanelJugador PanelJugador;
    public SaveDatos saveData;
    public menus_interface menuInterface;
    public PanelJugador JugadorInfo;
    public TiendaScript TiendaMenus;
    public ControlManager ControlManag;
    public AudiosMenus audioMenus;
    public equipo Equipo;
    public Pc PcScritp;

    //scrip player
    public movimiento PlayerMov;

  /*  public  libreriaDeScrips()
        {
        inventario = GameObject.Find("objetos/objetos").GetComponent<Inventario>();
        Itens = GameObject.Find("objetos/objetos").GetComponent<Items>();
        SeleccionDeCriaturas = GameObject.Find("Crewild/Crewild").GetComponent<CrewildChoiseSelect>();
        Batalla = GameObject.Find("baltle interfaceC/baltle interface").GetComponent<animationScritpBatle>();
        Ataques = GameObject.Find("Game Manager").GetComponent<AllAttacks>();
        Crewilldslist = GameObject.Find("Game Manager").GetComponent<AllCrewild>();
        todasLasPosturas = GameObject.Find("Game Manager").GetComponent<AllPostura>();
        ControlManag = GameObject.Find("Game Manager").GetComponent<ControlManager>();
        PanelJugador = GameObject.Find("Jugador/Jugador").GetComponent<PanelJugador>();
        informacionCrewild = GameObject.Find("informacion/estadisticas").GetComponent<informacionCrewild>();
        saveData = GameObject.Find("guardado/panel").GetComponent<SaveDatos>();
        menuInterface = GameObject.Find("Game Manager").GetComponent<menus_interface>();
        PlayerMov = GameObject.Find("personaje").GetComponent<movimiento>();
        TiendaMenus = FindObjectOfType<TiendaScript>();
        audioMenus = GameObject.Find("Game Manager").GetComponent<AudiosMenus>();
        JugadorInfo = FindObjectOfType<PanelJugador>();
        Equipo = FindObjectOfType<equipo>();
        PcScritp = FindObjectOfType<Pc>();
    } */

    public void Awake()
    {
        inventario = GameObject.Find("objetos/objetos").GetComponent<Inventario>();
        Itens = GameObject.Find("objetos/objetos").GetComponent<Items>();
        SeleccionDeCriaturas = GameObject.Find("Crewild/Crewild").GetComponent<CrewildChoiseSelect>();
        Batalla = GameObject.Find("baltle interfaceC/baltle interface").GetComponent<animationScritpBatle>();
        Ataques = GameObject.Find("Game Manager").GetComponent<AllAttacks>();
        Crewilldslist = GameObject.Find("Game Manager").GetComponent<AllCrewild>();
        todasLasPosturas = GameObject.Find("Game Manager").GetComponent<AllPostura>();
        ControlManag = GameObject.Find("Game Manager").GetComponent<ControlManager>();
        PanelJugador = GameObject.Find("Jugador/Jugador").GetComponent<PanelJugador>();
        informacionCrewild = GameObject.Find("informacion/estadisticas").GetComponent<informacionCrewild>();
        saveData = GameObject.Find("guardado/panel").GetComponent<SaveDatos>();
        menuInterface = GameObject.Find("Game Manager").GetComponent<menus_interface>();
        PlayerMov = GameObject.Find("personaje").GetComponent<movimiento>();
        TiendaMenus = FindObjectOfType<TiendaScript>();
        audioMenus = GameObject.FindObjectOfType<AudiosMenus>();
        JugadorInfo = FindObjectOfType<PanelJugador>();
        Equipo = FindObjectOfType<equipo>();
        PcScritp = FindObjectOfType<Pc>();
    }
   
}
