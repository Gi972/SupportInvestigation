﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.Model;

namespace SupportInvestigation.Models.InterfaceModel
{
    public interface IRepoHypothesis
    {


        //-------------------------------------------------------------------------------------------//
        //------------------------------------REQUETES INVESTIGATIONS--------------------------------//
        //-------------------------------------------------------------------------------------------//


        //Add
        void Add(Hypothesis Hypothese);

        //Update

        void Update(Hypothesis investigation);

        //Delete
        void Delete(Hypothesis investigation);

        //Get

        Hypothesis GetInvestigation(int id);

        List<Hypothesis> GetAllInvestigation();



        /// <summary>
        /// Retourne la liste des dernières investigations rentrées en base
        /// </summary>
        /// <returns></returns>
        List<Hypothesis> GetLastInvestigation();

        /// <summary>
        /// retourne la liste des investigations en cours
        /// </summary>
        /// <returns></returns>
        List<Hypothesis> GetAllInvestigationInProgress();

        /// <summary>
        /// Retourne la liste des investigation avec jointure des tables marchands et user a l'aide d'une procédure stockées
        /// </summary>
        /// <returns></returns>
        //List<Hypothesis> GetHomeInvestigation();


        ////TODO Vérifier que la requête n'a pas déja été défini avec GetTicketBelongToInvestigation(int id);
        List<Hypothesis> GestInvestigationByTicket(int idTicket);

        void Save();

    }
}