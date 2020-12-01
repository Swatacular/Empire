using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld.Planet;
using RimWorld;
using UnityEngine;
using Verse;
using HarmonyLib;
using Verse.AI;
using Verse.AI.Group;
using System.IO;

namespace FactionColonies
{
    public static class MilitaryAI
    {

        public static void SquadAI(ref MercenarySquadFC squad)
        {


            FactionFC factionfc = Find.World.GetComponent<FactionFC>();
            MilitaryCustomizationUtil militaryCustomizationUtil = factionfc.militaryCustomizationUtil;
            bool deployed = false;


            //Log.Message("1");
            
            foreach (Mercenary merc in squad.DeployedMercenaries)
            {


                //If pawn is up and moving, not downed.
                if (merc.pawn.Map != null && merc.pawn.health.State == PawnHealthState.Mobile)
                {
                    //set hitmap if not already
                    if (!(squad.hitMap))
                    {
                        squad.hitMap = true;
                    }

                   

                    //Log.Message(merc.pawn.CurJob.ToString());

                    //If in combat
                    //Log.Message("Start - Fight");
                    JobGiver_AIFightEnemies jobGiver = new JobGiver_AIFightEnemies();
                    ThinkResult result = jobGiver.TryIssueJobPackage(merc.pawn, new JobIssueParams());
                    bool isValid = result.IsValid;


                    if (isValid)
                    {
                        //Log.Message("Success");
                        if ((merc.pawn.jobs.curJob.def == JobDefOf.Goto || merc.pawn.jobs.curJob.def != result.Job.def) && merc.pawn.jobs.curJob.def.defName != "ReloadWeapon" && merc.pawn.jobs.curJob.def.defName != "ReloadTurret")
                        {
                            merc.pawn.jobs.StartJob(result.Job, JobCondition.Ongoing);
                            //Log.Message(result.Job.ToString());
                        }
                    }
                    else
                    {
                        //Log.Message("Fail");
                        if (squad.timeDeployed + 30000 >= Find.TickManager.TicksGame)
                        {
                            if (squad.order == MilitaryOrders.Standby)
                            {
                                //Log.Message("Standby");
                                merc.pawn.mindState.forcedGotoPosition = squad.orderLocation;
                                JobGiver_ForcedGoto jobGiver_Standby = new JobGiver_ForcedGoto();
                                ThinkResult resultStandby = jobGiver_Standby.TryIssueJobPackage(merc.pawn, new JobIssueParams());
                                bool isValidStandby = resultStandby.IsValid;
                                if (isValidStandby)
                                {
                                    //Log.Message("valid");
                                    merc.pawn.jobs.StartJob(resultStandby.Job, JobCondition.InterruptForced);

                                    
                                }
                            }
                            else
                            if (squad.order == MilitaryOrders.Attack)
                            {
                                //Log.Message("Attack");
                                //If time is up, leave, else go home
                                JobGiver_AIGotoNearestHostile jobGiver_Move = new JobGiver_AIGotoNearestHostile();
                                ThinkResult resultMove = jobGiver_Move.TryIssueJobPackage(merc.pawn, new JobIssueParams());
                                bool isValidMove = resultMove.IsValid;
                                //Log.Message(resultMove.ToString());
                                if (isValidMove)
                                {
                                    merc.pawn.jobs.StartJob(resultMove.Job, JobCondition.InterruptForced);
                                }
                                else
                                {

                                }
                            }
                            else
                            if (squad.order == MilitaryOrders.Leave)
                            {
                                JobGiver_ExitMapBest jobGiver_Rescue = new JobGiver_ExitMapBest();
                                ThinkResult resultLeave = jobGiver_Rescue.TryIssueJobPackage(merc.pawn, new JobIssueParams());
                                bool isValidLeave = resultLeave.IsValid;

                                if (isValidLeave)
                                {
                                    merc.pawn.jobs.StartJob(resultLeave.Job, JobCondition.InterruptForced);
                                }
                            }
                            else
                            if (squad.order == MilitaryOrders.RecoverWounded)
                            {
                                JobGiver_RescueNearby jobGiver_Rescue = new JobGiver_RescueNearby();
                                ThinkResult resultRescue = jobGiver_Rescue.TryIssueJobPackage(merc.pawn, new JobIssueParams());
                                bool isValidRescue = resultRescue.IsValid;

                                if (isValidRescue)
                                {
                                    merc.pawn.jobs.StartJob(resultRescue.Job, JobCondition.InterruptForced);
                                }
                            }
                        }
                    }



                    //end of if pawn is mobile
                }
                else
                {
                    //if pawn is down,dead, or gone

                    //Log.Message("Not Deployed");
                    //not deployed
                }


                if (merc.pawn.health.Dead)
                {

                    squad.removeDroppedEquipment();

                    //Log.Message("Passing to dead Pawns");
                    squad.PassPawnToDeadMercenaries(merc);

                    squad.hasDead = true;
                }


                if (merc.pawn.Map != null && !(merc.pawn.health.Dead))
                {
                    //squad.isDeployed
                    deployed = true;
                }
            }


            //Log.Message("2");
            foreach (Mercenary animal in squad.DeployedMercenaryAnimals)
            {
                if (animal.pawn.Map != null && animal.pawn.health.State == PawnHealthState.Mobile)
                {
                    animal.pawn.mindState.duty = new PawnDuty();
                    animal.pawn.mindState.duty.def = DutyDefOf.Defend;
                    animal.pawn.mindState.duty.attackDownedIfStarving = false;
                    //animal.pawn.mindState.duty.radius = 2;
                    animal.pawn.mindState.duty.focus = animal.handler.pawn;
                    //If master is not dead
                    JobGiver_AIFightEnemies jobGiver = new JobGiver_AIFightEnemies();
                    ThinkResult result = jobGiver.TryIssueJobPackage(animal.pawn, new JobIssueParams());
                    bool isValid = result.IsValid;
                    if (isValid)
                    {
                        //Log.Message("att");
                        if (animal.pawn.jobs.curJob.def != result.Job.def)
                        {
                            animal.pawn.jobs.StartJob(result.Job, JobCondition.InterruptForced);
                        }
                    }
                    else
                    {
                        animal.pawn.mindState.duty.def = DutyDefOf.Defend;
                        animal.pawn.mindState.duty.radius = 2;
                        animal.pawn.mindState.duty.focus = animal.handler.pawn;
                        //if defend master not valid, follow master
                        JobGiver_AIFollowEscortee jobGiverFollow = new JobGiver_AIFollowEscortee();
                        ThinkResult resultFollow = jobGiverFollow.TryIssueJobPackage(animal.pawn, new JobIssueParams());
                        bool isValidFollow = resultFollow.IsValid;
                        if (isValidFollow)
                        {
                            //Log.Message("foloor");
                            if (animal.pawn.jobs.curJob.def != resultFollow.Job.def)
                            {
                                animal.pawn.jobs.StartJob(resultFollow.Job, JobCondition.Ongoing);
                            }
                        } else
                        {
                            JobGiver_ExitMapBest jobGiver_Rescue = new JobGiver_ExitMapBest();
                            ThinkResult resultLeave = jobGiver_Rescue.TryIssueJobPackage(animal.pawn, new JobIssueParams());
                            bool isValidLeave = resultLeave.IsValid;

                            if (isValidLeave)
                            {
                                animal.pawn.jobs.StartJob(resultLeave.Job, JobCondition.InterruptForced);
                            }
                        }
                    }



                }
                if (animal.pawn.health.Dead || animal.pawn.health.Downed)
                {
                    //Log.Message("Despawning dead");
                    //animal.pawn.DeSpawn();

                }

                if (animal.pawn.Map != null && !(animal.pawn.health.Dead))
                {
                    deployed = true;
                }
            }

            //Log.Message("3");

            if (!(deployed) && squad.hitMap)
            {
                squad.hasLord = false;
                squad.isDeployed = false;
                squad.removeDroppedEquipment();
                squad.getSettlement.cooldownMilitary();
                //Log.Message("Reseting Squad");
                militaryCustomizationUtil.checkMilitaryUtilForErrors();
                squad.OutfitSquad(squad.outfit);
                squad.hitMap = false;

                if (squad.map != null)
                {
                    squad.map.lordManager.RemoveLord(squad.lord);
                    squad.lord = null;
                    squad.map = null;
                }

            }
        }
    }
}
