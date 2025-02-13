using HarmonyLib;
using System;
using static LasMonjas.LasMonjas;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using LasMonjas.Objects;
using LasMonjas.Core;

namespace LasMonjas.Patches
{
    [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.OnDestroy))]
    class IntroCutsceneOnDestroyPatch
    {
        public static void Prefix(IntroCutscene __instance) {

            // Generate alive player icons for Pyromaniac
            int playerCounter = 0;
            if (PlayerControl.LocalPlayer != null && HudManager.Instance != null) {
                Vector3 bottomLeft = new Vector3(-HudManager.Instance.UseButton.transform.localPosition.x, HudManager.Instance.UseButton.transform.localPosition.y, HudManager.Instance.UseButton.transform.localPosition.z);
                foreach (PlayerControl p in PlayerControl.AllPlayerControls) {
                    GameData.PlayerInfo data = p.Data;
                    PoolablePlayer player = UnityEngine.Object.Instantiate<PoolablePlayer>(__instance.PlayerPrefab, HudManager.Instance.transform);
                    PlayerControl.SetPlayerMaterialColors(data.DefaultOutfit.ColorId, player.Body);
                    DestroyableSingleton<HatManager>.Instance.SetSkin(player.Skin.layer, data.DefaultOutfit.SkinId);
                    player.HatSlot.SetHat(data.DefaultOutfit.HatId, data.DefaultOutfit.ColorId);
                    PlayerControl.SetPetImage(data.DefaultOutfit.PetId, data.DefaultOutfit.ColorId, player.PetSlot);
                    player.NameText.text = data.PlayerName;
                    player.SetFlipX(true);
                    MapOptions.playerIcons[p.PlayerId] = player;

                    if (PlayerControl.LocalPlayer == Pyromaniac.pyromaniac && p != Pyromaniac.pyromaniac) {
                        player.transform.localPosition = bottomLeft + new Vector3(-0.25f, -0.25f, 0) + Vector3.right * playerCounter++ * 0.35f;
                        player.transform.localScale = Vector3.one * 0.2f;
                        player.setSemiTransparent(true);
                        player.gameObject.SetActive(true);
                    }
                    else {
                        player.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    [HarmonyPatch]
    class IntroPatch
    {
        public static void setupIntroTeamIcons(IntroCutscene __instance, ref Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam) {

            SoundManager.Instance.StopSound(CustomMain.customAssets.lobbyMusic);
            
            if (CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode) {
                SoundManager.Instance.PlaySound(CustomMain.customAssets.captureTheFlagMusic, true, 25f);
                // Intro capture the flag teams
                var redTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                if (PlayerControl.LocalPlayer == CaptureTheFlag.redplayer01 || PlayerControl.LocalPlayer == CaptureTheFlag.redplayer02 || PlayerControl.LocalPlayer == CaptureTheFlag.redplayer03 || PlayerControl.LocalPlayer == CaptureTheFlag.redplayer04 || PlayerControl.LocalPlayer == CaptureTheFlag.redplayer05 || PlayerControl.LocalPlayer == CaptureTheFlag.redplayer06 || PlayerControl.LocalPlayer == CaptureTheFlag.redplayer07) {
                    redTeam.Add(PlayerControl.LocalPlayer);
                    yourTeam = redTeam;
                }
                var blueTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                if (PlayerControl.LocalPlayer == CaptureTheFlag.blueplayer01 || PlayerControl.LocalPlayer == CaptureTheFlag.blueplayer02 || PlayerControl.LocalPlayer == CaptureTheFlag.blueplayer03 || PlayerControl.LocalPlayer == CaptureTheFlag.blueplayer04 || PlayerControl.LocalPlayer == CaptureTheFlag.blueplayer05 || PlayerControl.LocalPlayer == CaptureTheFlag.blueplayer06 || PlayerControl.LocalPlayer == CaptureTheFlag.blueplayer07) {
                    blueTeam.Add(PlayerControl.LocalPlayer);
                    yourTeam = blueTeam;
                }
                if (PlayerControl.LocalPlayer == CaptureTheFlag.stealerPlayer) {
                    var greyTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                    greyTeam.Add(PlayerControl.LocalPlayer);
                    yourTeam = greyTeam;
                }
            }
            else if (PoliceAndThief.policeAndThiefMode && !CaptureTheFlag.captureTheFlagMode && !KingOfTheHill.kingOfTheHillMode) {
                SoundManager.Instance.PlaySound(CustomMain.customAssets.policeAndThiefMusic, true, 25f);
                // Intro police and thiefs teams
                var thiefTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                if (PlayerControl.LocalPlayer == PoliceAndThief.thiefplayer01 || PlayerControl.LocalPlayer == PoliceAndThief.thiefplayer02 || PlayerControl.LocalPlayer == PoliceAndThief.thiefplayer03 || PlayerControl.LocalPlayer == PoliceAndThief.thiefplayer04 || PlayerControl.LocalPlayer == PoliceAndThief.thiefplayer05 || PlayerControl.LocalPlayer == PoliceAndThief.thiefplayer06 || PlayerControl.LocalPlayer == PoliceAndThief.thiefplayer07 || PlayerControl.LocalPlayer == PoliceAndThief.thiefplayer08 || PlayerControl.LocalPlayer == PoliceAndThief.thiefplayer09 || PlayerControl.LocalPlayer == PoliceAndThief.thiefplayer10) {
                    thiefTeam.Add(PlayerControl.LocalPlayer);
                    yourTeam = thiefTeam;
                }
                var policeTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                if (PlayerControl.LocalPlayer == PoliceAndThief.policeplayer01 || PlayerControl.LocalPlayer == PoliceAndThief.policeplayer02 || PlayerControl.LocalPlayer == PoliceAndThief.policeplayer03 || PlayerControl.LocalPlayer == PoliceAndThief.policeplayer04 || PlayerControl.LocalPlayer == PoliceAndThief.policeplayer05) {
                    policeTeam.Add(PlayerControl.LocalPlayer);
                    yourTeam = policeTeam;
                }
            }
            else if (KingOfTheHill.kingOfTheHillMode && !PoliceAndThief.policeAndThiefMode && !CaptureTheFlag.captureTheFlagMode) {
                SoundManager.Instance.PlaySound(CustomMain.customAssets.kingOfTheHillMusic, true, 25f);
                // Intro king of the hill teams
                var greenTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                if (PlayerControl.LocalPlayer == KingOfTheHill.greenKingplayer || PlayerControl.LocalPlayer == KingOfTheHill.greenplayer01 || PlayerControl.LocalPlayer == KingOfTheHill.greenplayer02 || PlayerControl.LocalPlayer == KingOfTheHill.greenplayer03 || PlayerControl.LocalPlayer == KingOfTheHill.greenplayer04 || PlayerControl.LocalPlayer == KingOfTheHill.greenplayer05 || PlayerControl.LocalPlayer == KingOfTheHill.greenplayer06) {
                    greenTeam.Add(PlayerControl.LocalPlayer);
                    yourTeam = greenTeam;
                }
                var yellowTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                if (PlayerControl.LocalPlayer == KingOfTheHill.yellowKingplayer || PlayerControl.LocalPlayer == KingOfTheHill.yellowplayer01 || PlayerControl.LocalPlayer == KingOfTheHill.yellowplayer02 || PlayerControl.LocalPlayer == KingOfTheHill.yellowplayer03 || PlayerControl.LocalPlayer == KingOfTheHill.yellowplayer04 || PlayerControl.LocalPlayer == KingOfTheHill.yellowplayer05 || PlayerControl.LocalPlayer == KingOfTheHill.yellowplayer06) {
                    yellowTeam.Add(PlayerControl.LocalPlayer);
                    yourTeam = yellowTeam;
                }
                if (PlayerControl.LocalPlayer == KingOfTheHill.usurperPlayer) {
                    var greyTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                    greyTeam.Add(PlayerControl.LocalPlayer);
                    yourTeam = greyTeam;
                }
            }
            else {
                // Intro solo teams (rebels and neutrals)
                if (PlayerControl.LocalPlayer == Joker.joker || PlayerControl.LocalPlayer == RoleThief.rolethief || PlayerControl.LocalPlayer == Pyromaniac.pyromaniac || PlayerControl.LocalPlayer == TreasureHunter.treasureHunter || PlayerControl.LocalPlayer == Devourer.devourer || PlayerControl.LocalPlayer == Renegade.renegade || PlayerControl.LocalPlayer == BountyHunter.bountyhunter || PlayerControl.LocalPlayer == Trapper.trapper || PlayerControl.LocalPlayer == Yinyanger.yinyanger || PlayerControl.LocalPlayer == Challenger.challenger) {
                    var soloTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                    soloTeam.Add(PlayerControl.LocalPlayer);
                    yourTeam = soloTeam;
                }
                
                if (MapOptions.activateMusic) {
                    SoundManager.Instance.PlaySound(CustomMain.customAssets.tasksCalmMusic, true, 25f);
                }
            }
        }

        public static void setupIntroTeam(IntroCutscene __instance, ref Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam) {
            List<RoleInfo> infos = RoleInfo.getRoleInfoForPlayer(PlayerControl.LocalPlayer);
            RoleInfo roleInfo = infos.Where(info => info.roleId != RoleId.Lover).FirstOrDefault();
            if (roleInfo == null) return;
            if (roleInfo.isNeutral) {
                var neutralColor = new Color32(76, 84, 78, 255);
                __instance.BackgroundBar.material.color = neutralColor;
                __instance.TeamTitle.text = "Neutral";
                __instance.TeamTitle.color = neutralColor;
            }
            else if (roleInfo.isRebel) {
                var rebelColor = new Color32(79, 125, 0, 255);
                __instance.BackgroundBar.material.color = rebelColor;
                __instance.TeamTitle.text = "Rebel";
                __instance.TeamTitle.color = rebelColor;
            }
        }

        [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.SetUpRoleText))]
        class SetUpRoleTextPatch
        {
            public static void Postfix(IntroCutscene __instance) {
                if (!CustomOptionHolder.activateRoles.getBool()) return; 

                List<RoleInfo> infos = RoleInfo.getRoleInfoForPlayer(PlayerControl.LocalPlayer);
                RoleInfo roleInfo = infos.Where(info => info.roleId != RoleId.Lover).FirstOrDefault();

                if (roleInfo != null) {
                    __instance.RoleText.text = roleInfo.name;
                    __instance.RoleText.color = roleInfo.color;
                    __instance.RoleBlurbText.text = roleInfo.introDescription;
                    __instance.RoleBlurbText.color = roleInfo.color;
                }

                if (infos.Any(info => info.roleId == RoleId.Lover)) {
                    PlayerControl otherLover = PlayerControl.LocalPlayer == Modifiers.lover1 ? Modifiers.lover2 : Modifiers.lover1;
                    __instance.RoleBlurbText.text = PlayerControl.LocalPlayer.Data.Role.IsImpostor ? "<color=#FF00D1FF>Lover</color><color=#FF0000FF>stor</color>" : "<color=#FF00D1FF>Lover</color>";
                    __instance.RoleBlurbText.color = PlayerControl.LocalPlayer.Data.Role.IsImpostor ? Color.white : Modifiers.loverscolor;
                    __instance.ImpostorText.text = Helpers.cs(Modifiers.loverscolor, $"♥ Survive as a couple with {otherLover?.Data?.PlayerName ?? ""} ♥");
                    __instance.ImpostorText.gameObject.SetActive(true);
                    __instance.BackgroundBar.material.color = Modifiers.loverscolor;
                }

                // Create the doorlog access from anywhere to the Vigilant on MiraHQ
                if (PlayerControl.GameOptions.MapId == 1 && Vigilant.vigilantMira != null && Vigilant.vigilantMira == PlayerControl.LocalPlayer && !Vigilant.createdDoorLog) {
                    GameObject vigilantDoorLog = GameObject.Find("SurvLogConsole");
                    Vigilant.doorLog = GameObject.Instantiate(vigilantDoorLog, Vigilant.vigilantMira.transform);
                    Vigilant.doorLog.name = "VigilantDoorLog";
                    Vigilant.doorLog.layer = 8; // Assign player layer to ignore collisions
                    Vigilant.doorLog.GetComponent<SpriteRenderer>().enabled = false;
                    Vigilant.doorLog.transform.localPosition = new Vector2(0, -0.5f);
                    Vigilant.createdDoorLog = true;
                }

                // Create the duel arena if there's a Challenger
                if (Challenger.challenger != null) {
                    if (PlayerControl.LocalPlayer != null && !createdduelarena) {
                        GameObject duelArena = GameObject.Instantiate(CustomMain.customAssets.challengerDuelArena, PlayerControl.LocalPlayer.transform.parent);
                        duelArena.name = "duelArena";
                        duelArena.transform.position = new Vector3(40, 0f, 1f);
                        createdduelarena = true;
                    }
                }

                // Remove vitals use from Polus and Airship for TimeTraveler
                if (TimeTraveler.timeTraveler != null && PlayerControl.LocalPlayer == TimeTraveler.timeTraveler) {
                    if (PlayerControl.GameOptions.MapId == 2) {
                        GameObject vitals = GameObject.Find("panel_vitals");
                        vitals.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    if (PlayerControl.GameOptions.MapId == 4) {
                        GameObject vitals = GameObject.Find("panel_vitals");
                        vitals.GetComponent<CircleCollider2D>().enabled = false;
                    }
                }

                // Remove the swipe card task
                clearSwipeCardTask();

                // Remove airship doors
                removeAirshipDoors();

                // Activate sensei map
                activateSenseiMap();

                // Capture the flag
                if (CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode) {
                    switch (PlayerControl.GameOptions.MapId) {
                        // Skeld
                        case 0:
                            if (activatedSensei) {
                                if (PlayerControl.LocalPlayer == CaptureTheFlag.stealerPlayer) {
                                    CaptureTheFlag.stealerPlayer.transform.position = new Vector3(-3.65f, 5f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(CaptureTheFlag.stealerPlayer);
                                }
                                
                                foreach (PlayerControl player in CaptureTheFlag.redteamFlag) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(-17.5f, -1.15f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                foreach (PlayerControl player in CaptureTheFlag.blueteamFlag) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(7.7f, -0.95f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                if (PlayerControl.LocalPlayer != null && !createdcapturetheflag) {
                                    GameObject redflag = GameObject.Instantiate(CustomMain.customAssets.redflag, PlayerControl.LocalPlayer.transform.parent);
                                    redflag.name = "redflag";
                                    redflag.transform.position = new Vector3(-17.5f, -1.35f, 0.5f);
                                    CaptureTheFlag.redflag = redflag;
                                    GameObject redflagbase = GameObject.Instantiate(CustomMain.customAssets.redflagbase, PlayerControl.LocalPlayer.transform.parent);
                                    redflagbase.name = "redflagbase";
                                    redflagbase.transform.position = new Vector3(-17.5f, -1.4f, 1f);
                                    CaptureTheFlag.redflagbase = redflagbase;
                                    GameObject blueflag = GameObject.Instantiate(CustomMain.customAssets.blueflag, PlayerControl.LocalPlayer.transform.parent);
                                    blueflag.name = "blueflag";
                                    blueflag.transform.position = new Vector3(7.7f, -1.15f, 0.5f);
                                    CaptureTheFlag.blueflag = blueflag;
                                    GameObject blueflagbase = GameObject.Instantiate(CustomMain.customAssets.blueflagbase, PlayerControl.LocalPlayer.transform.parent);
                                    blueflagbase.name = "blueflagbase";
                                    blueflagbase.transform.position = new Vector3(7.7f, -1.2f, 1f);
                                    CaptureTheFlag.blueflagbase = blueflagbase;
                                    createdcapturetheflag = true;

                                    // Remove camera use and admin table on Skeld
                                    GameObject cameraStand = GameObject.Find("SurvConsole");
                                    cameraStand.GetComponent<PolygonCollider2D>().enabled = false;
                                    GameObject admin = GameObject.Find("MapRoomConsole");
                                    admin.GetComponent<CircleCollider2D>().enabled = false;
                                }
                            }
                            else {
                                if (PlayerControl.LocalPlayer == CaptureTheFlag.stealerPlayer) {
                                    CaptureTheFlag.stealerPlayer.transform.position = new Vector3(6.35f, -7.5f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(CaptureTheFlag.stealerPlayer);
                                }
                                
                                foreach (PlayerControl player in CaptureTheFlag.redteamFlag) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(-20.5f, -5.15f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                foreach (PlayerControl player in CaptureTheFlag.blueteamFlag) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(16.5f, -4.45f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                if (PlayerControl.LocalPlayer != null && !createdcapturetheflag) {
                                    GameObject redflag = GameObject.Instantiate(CustomMain.customAssets.redflag, PlayerControl.LocalPlayer.transform.parent);
                                    redflag.name = "redflag";
                                    redflag.transform.position = new Vector3(-20.5f, -5.35f, 0.5f);
                                    CaptureTheFlag.redflag = redflag;
                                    GameObject redflagbase = GameObject.Instantiate(CustomMain.customAssets.redflagbase, PlayerControl.LocalPlayer.transform.parent);
                                    redflagbase.name = "redflagbase";
                                    redflagbase.transform.position = new Vector3(-20.5f, -5.4f, 1f);
                                    CaptureTheFlag.redflagbase = redflagbase;
                                    GameObject blueflag = GameObject.Instantiate(CustomMain.customAssets.blueflag, PlayerControl.LocalPlayer.transform.parent);
                                    blueflag.name = "blueflag";
                                    blueflag.transform.position = new Vector3(16.5f, -4.65f, 0.5f);
                                    CaptureTheFlag.blueflag = blueflag;
                                    GameObject blueflagbase = GameObject.Instantiate(CustomMain.customAssets.blueflagbase, PlayerControl.LocalPlayer.transform.parent);
                                    blueflagbase.name = "blueflagbase";
                                    blueflagbase.transform.position = new Vector3(16.5f, -4.7f, 1f);
                                    CaptureTheFlag.blueflagbase = blueflagbase;
                                    createdcapturetheflag = true;

                                    // Remove camera use and admin table on Skeld
                                    GameObject cameraStand = GameObject.Find("SurvConsole");
                                    cameraStand.GetComponent<PolygonCollider2D>().enabled = false;
                                    GameObject admin = GameObject.Find("MapRoomConsole");
                                    admin.GetComponent<CircleCollider2D>().enabled = false;
                                }
                            }
                            break;
                        // MiraHQ
                        case 1:
                            if (PlayerControl.LocalPlayer == CaptureTheFlag.stealerPlayer) {
                                CaptureTheFlag.stealerPlayer.transform.position = new Vector3(17.75f, 24f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(CaptureTheFlag.stealerPlayer);
                            }
                            
                            foreach (PlayerControl player in CaptureTheFlag.redteamFlag) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(2.53f, 10.75f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in CaptureTheFlag.blueteamFlag) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(23.25f, 5.25f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdcapturetheflag) {
                                GameObject redflag = GameObject.Instantiate(CustomMain.customAssets.redflag, PlayerControl.LocalPlayer.transform.parent);
                                redflag.name = "redflag";
                                redflag.transform.position = new Vector3(2.525f, 10.55f, 0.5f);
                                CaptureTheFlag.redflag = redflag;
                                GameObject redflagbase = GameObject.Instantiate(CustomMain.customAssets.redflagbase, PlayerControl.LocalPlayer.transform.parent);
                                redflagbase.name = "redflagbase";
                                redflagbase.transform.position = new Vector3(2.53f, 10.5f, 1f);
                                CaptureTheFlag.redflagbase = redflagbase;
                                GameObject blueflag = GameObject.Instantiate(CustomMain.customAssets.blueflag, PlayerControl.LocalPlayer.transform.parent);
                                blueflag.name = "blueflag";
                                blueflag.transform.position = new Vector3(23.25f, 5.05f, 0.5f);
                                CaptureTheFlag.blueflag = blueflag;
                                GameObject blueflagbase = GameObject.Instantiate(CustomMain.customAssets.blueflagbase, PlayerControl.LocalPlayer.transform.parent);
                                blueflagbase.name = "blueflagbase";
                                blueflagbase.transform.position = new Vector3(23.25f, 5f, 1f);
                                CaptureTheFlag.blueflagbase = blueflagbase;
                                createdcapturetheflag = true;

                                // Remove Doorlog use, Decontamination doors and admin table on MiraHQ
                                GameObject DoorLog = GameObject.Find("SurvLogConsole");
                                DoorLog.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject deconUpperDoor = GameObject.Find("UpperDoor");
                                deconUpperDoor.SetActive(false);
                                GameObject deconLowerDoor = GameObject.Find("LowerDoor");
                                deconLowerDoor.SetActive(false);
                                GameObject deconUpperDoorPanelTop = GameObject.Find("DeconDoorPanel-Top");
                                deconUpperDoorPanelTop.SetActive(false);
                                GameObject deconUpperDoorPanelHigh = GameObject.Find("DeconDoorPanel-High");
                                deconUpperDoorPanelHigh.SetActive(false);
                                GameObject deconUpperDoorPanelBottom = GameObject.Find("DeconDoorPanel-Bottom");
                                deconUpperDoorPanelBottom.SetActive(false);
                                GameObject deconUpperDoorPanelLow = GameObject.Find("DeconDoorPanel-Low");
                                deconUpperDoorPanelLow.SetActive(false);
                                GameObject admin = GameObject.Find("AdminMapConsole");
                                admin.GetComponent<CircleCollider2D>().enabled = false;
                            }
                            break;
                        // Polus
                        case 2:
                            if (PlayerControl.LocalPlayer == CaptureTheFlag.stealerPlayer) {
                                CaptureTheFlag.stealerPlayer.transform.position = new Vector3(31.75f, -13f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(CaptureTheFlag.stealerPlayer);
                            }
                            
                            foreach (PlayerControl player in CaptureTheFlag.redteamFlag) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(36.4f, -21.5f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in CaptureTheFlag.blueteamFlag) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(5.4f, -9.45f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdcapturetheflag) {
                                GameObject redflag = GameObject.Instantiate(CustomMain.customAssets.redflag, PlayerControl.LocalPlayer.transform.parent);
                                redflag.name = "redflag";
                                redflag.transform.position = new Vector3(36.4f, -21.7f, 0.5f);
                                CaptureTheFlag.redflag = redflag;
                                GameObject redflagbase = GameObject.Instantiate(CustomMain.customAssets.redflagbase, PlayerControl.LocalPlayer.transform.parent);
                                redflagbase.name = "redflagbase";
                                redflagbase.transform.position = new Vector3(36.4f, -21.75f, 1f);
                                CaptureTheFlag.redflagbase = redflagbase;
                                GameObject blueflag = GameObject.Instantiate(CustomMain.customAssets.blueflag, PlayerControl.LocalPlayer.transform.parent);
                                blueflag.name = "blueflag";
                                blueflag.transform.position = new Vector3(5.4f, -9.65f, 0.5f);
                                CaptureTheFlag.blueflag = blueflag;
                                GameObject blueflagbase = GameObject.Instantiate(CustomMain.customAssets.blueflagbase, PlayerControl.LocalPlayer.transform.parent);
                                blueflagbase.name = "blueflagbase";
                                blueflagbase.transform.position = new Vector3(5.4f, -9.7f, 1f);
                                CaptureTheFlag.blueflagbase = blueflagbase;
                                createdcapturetheflag = true;

                                // Remove Decon doors, camera use, vitals, admin tables on Polus
                                GameObject lowerdecon = GameObject.Find("LowerDecon");
                                lowerdecon.SetActive(false);
                                GameObject upperdecon = GameObject.Find("UpperDecon");
                                upperdecon.SetActive(false);
                                GameObject survCameras = GameObject.Find("Surv_Panel");
                                survCameras.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject vitals = GameObject.Find("panel_vitals");
                                vitals.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject adminone = GameObject.Find("panel_map");
                                adminone.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject admintwo = GameObject.Find("panel_map (1)");
                                admintwo.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject ramp = GameObject.Find("ramp");
                                ramp.transform.position = new Vector3(ramp.transform.position.x, ramp.transform.position.y, 0.75f);
                                GameObject bathroomVent = GameObject.Find("BathroomVent");
                                bathroomVent.transform.position = new Vector3(34, -10.3f, bathroomVent.transform.position.z);
                            }
                            break;
                        // Dlesk
                        case 3:
                            if (PlayerControl.LocalPlayer == CaptureTheFlag.stealerPlayer) {
                                CaptureTheFlag.stealerPlayer.transform.position = new Vector3(-6.35f, -7.5f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(CaptureTheFlag.stealerPlayer);
                            }
                            
                            foreach (PlayerControl player in CaptureTheFlag.redteamFlag) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(20.5f, -5.15f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in CaptureTheFlag.blueteamFlag) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(-16.5f, -4.45f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdcapturetheflag) {
                                GameObject redflag = GameObject.Instantiate(CustomMain.customAssets.redflag, PlayerControl.LocalPlayer.transform.parent);
                                redflag.name = "redflag";
                                redflag.transform.position = new Vector3(20.5f, -5.35f, 0.5f);
                                CaptureTheFlag.redflag = redflag;
                                GameObject redflagbase = GameObject.Instantiate(CustomMain.customAssets.redflagbase, PlayerControl.LocalPlayer.transform.parent);
                                redflagbase.name = "redflagbase";
                                redflagbase.transform.position = new Vector3(20.5f, -5.4f, 1f);
                                CaptureTheFlag.redflagbase = redflagbase;
                                GameObject blueflag = GameObject.Instantiate(CustomMain.customAssets.blueflag, PlayerControl.LocalPlayer.transform.parent);
                                blueflag.name = "blueflag";
                                blueflag.transform.position = new Vector3(-16.5f, -4.65f, 0.5f);
                                CaptureTheFlag.blueflag = blueflag;
                                GameObject blueflagbase = GameObject.Instantiate(CustomMain.customAssets.blueflagbase, PlayerControl.LocalPlayer.transform.parent);
                                blueflagbase.name = "blueflagbase";
                                blueflagbase.transform.position = new Vector3(-16.5f, -4.7f, 1f);
                                CaptureTheFlag.blueflagbase = blueflagbase;
                                createdcapturetheflag = true;

                                // Remove camera use and admin table on Dleks
                                GameObject cameraStand = GameObject.Find("SurvConsole");
                                cameraStand.GetComponent<PolygonCollider2D>().enabled = false;
                                GameObject admin = GameObject.Find("MapRoomConsole");
                                admin.GetComponent<CircleCollider2D>().enabled = false;
                            }
                            break;
                        // Airship
                        case 4:
                            if (PlayerControl.LocalPlayer == CaptureTheFlag.stealerPlayer) {
                                CaptureTheFlag.stealerPlayer.transform.position = new Vector3(10.25f, -15.35f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(CaptureTheFlag.stealerPlayer);
                            }
                            
                            foreach (PlayerControl player in CaptureTheFlag.redteamFlag) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(-17.5f, -1f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in CaptureTheFlag.blueteamFlag) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(33.6f, 1.45f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdcapturetheflag) {
                                GameObject redflag = GameObject.Instantiate(CustomMain.customAssets.redflag, PlayerControl.LocalPlayer.transform.parent);
                                redflag.name = "redflag";
                                redflag.transform.position = new Vector3(-17.5f, -1.2f, 0.5f);
                                CaptureTheFlag.redflag = redflag;
                                GameObject redflagbase = GameObject.Instantiate(CustomMain.customAssets.redflagbase, PlayerControl.LocalPlayer.transform.parent);
                                redflagbase.name = "redflagbase";
                                redflagbase.transform.position = new Vector3(-17.5f, -1.25f, 1f);
                                CaptureTheFlag.redflagbase = redflagbase;
                                GameObject blueflag = GameObject.Instantiate(CustomMain.customAssets.blueflag, PlayerControl.LocalPlayer.transform.parent);
                                blueflag.name = "blueflag";
                                blueflag.transform.position = new Vector3(33.6f, 1.25f, 0.5f);
                                CaptureTheFlag.blueflag = blueflag;
                                GameObject blueflagbase = GameObject.Instantiate(CustomMain.customAssets.blueflagbase, PlayerControl.LocalPlayer.transform.parent);
                                blueflagbase.name = "blueflagbase";
                                blueflagbase.transform.position = new Vector3(33.6f, 1.2f, 1f);
                                CaptureTheFlag.blueflagbase = blueflagbase;
                                createdcapturetheflag = true;

                                // Remove camera use, admin table, vitals, electrical doors on Airship
                                GameObject cameras = GameObject.Find("task_cams");
                                cameras.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject admin = GameObject.Find("panel_cockpit_map");
                                admin.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject vitals = GameObject.Find("panel_vitals");
                                vitals.GetComponent<CircleCollider2D>().enabled = false;
                                GameObject LeftDoorTop = GameObject.Find("LeftDoorTop");
                                LeftDoorTop.SetActive(false);
                                GameObject TopLeftVert = GameObject.Find("TopLeftVert");
                                TopLeftVert.SetActive(false);
                                GameObject TopLeftHort = GameObject.Find("TopLeftHort");
                                TopLeftHort.SetActive(false);
                                GameObject BottomHort = GameObject.Find("BottomHort");
                                BottomHort.SetActive(false);
                                GameObject TopCenterHort = GameObject.Find("TopCenterHort");
                                TopCenterHort.SetActive(false);
                                GameObject LeftVert = GameObject.Find("LeftVert");
                                LeftVert.SetActive(false);
                                GameObject RightVert = GameObject.Find("RightVert");
                                RightVert.SetActive(false);
                                GameObject TopRightVert = GameObject.Find("TopRightVert");
                                TopRightVert.SetActive(false);
                                GameObject TopRightHort = GameObject.Find("TopRightHort");
                                TopRightHort.SetActive(false);
                                GameObject BottomRightHort = GameObject.Find("BottomRightHort");
                                BottomRightHort.SetActive(false);
                                GameObject BottomRightVert = GameObject.Find("BottomRightVert");
                                BottomRightVert.SetActive(false);
                                GameObject LeftDoorBottom = GameObject.Find("LeftDoorBottom");
                                LeftDoorBottom.SetActive(false);
                            }
                            break;
                    }
                    new CustomMessage("Time Left: ", CaptureTheFlag.matchDuration, -1, -1.3f, 3);
                    new CustomMessage(CaptureTheFlag.flagpointCounter, CaptureTheFlag.matchDuration, -1, 1.9f, 5);

                    // Add Arrows pointing the flags
                    if (CaptureTheFlag.localRedFlagArrow.Count == 0) CaptureTheFlag.localRedFlagArrow.Add(new Arrow(Color.red));
                    CaptureTheFlag.localRedFlagArrow[0].arrow.SetActive(true);
                    if (CaptureTheFlag.localBlueFlagArrow.Count == 0) CaptureTheFlag.localBlueFlagArrow.Add(new Arrow(Color.blue));
                    CaptureTheFlag.localBlueFlagArrow[0].arrow.SetActive(true);
                }
                // Police And Thief          
                else if (PoliceAndThief.policeAndThiefMode && !CaptureTheFlag.captureTheFlagMode && !KingOfTheHill.kingOfTheHillMode) {
                    switch (PlayerControl.GameOptions.MapId) {
                        // Skeld
                        case 0:
                            if (activatedSensei) {
                                foreach (PlayerControl player in PoliceAndThief.policeTeam) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(-12f, 5f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(13.75f, -0.2f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                if (PlayerControl.LocalPlayer != null && !createdpoliceandthief) {
                                    GameObject cell = GameObject.Instantiate(CustomMain.customAssets.cell, PlayerControl.LocalPlayer.transform.parent);
                                    cell.name = "cell";
                                    cell.transform.position = new Vector3(-12f, 7.2f, 0.5f);
                                    cell.gameObject.layer = 9;
                                    cell.transform.GetChild(0).gameObject.layer = 9;
                                    PoliceAndThief.cell = cell;
                                    GameObject cellbutton = GameObject.Instantiate(CustomMain.customAssets.freethiefbutton, PlayerControl.LocalPlayer.transform.parent);
                                    cellbutton.name = "cellbutton";
                                    cellbutton.transform.position = new Vector3(-12f, 4.7f, 0.5f);
                                    PoliceAndThief.cellbutton = cellbutton;
                                    GameObject jewelbutton = GameObject.Instantiate(CustomMain.customAssets.jewelbutton, PlayerControl.LocalPlayer.transform.parent);
                                    jewelbutton.name = "jewelbutton";
                                    jewelbutton.transform.position = new Vector3(13.75f, -0.42f, 0.5f);
                                    PoliceAndThief.jewelbutton = jewelbutton;
                                    GameObject thiefspaceship = GameObject.Instantiate(CustomMain.customAssets.thiefspaceship, PlayerControl.LocalPlayer.transform.parent);
                                    thiefspaceship.name = "thiefspaceship";
                                    thiefspaceship.transform.position = new Vector3(17f, 0f, 0.6f);
                                    createdpoliceandthief = true;

                                    // Remove camera use and admin table on Skeld
                                    GameObject cameraStand = GameObject.Find("SurvConsole");
                                    cameraStand.GetComponent<PolygonCollider2D>().enabled = false;
                                    GameObject admin = GameObject.Find("MapRoomConsole");
                                    admin.GetComponent<CircleCollider2D>().enabled = false;

                                    // Spawn jewels
                                    GameObject jewel01 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel01.transform.position = new Vector3(6.95f, 4.95f, 1f);
                                    jewel01.name = "jewel01";
                                    PoliceAndThief.jewel01 = jewel01;
                                    GameObject jewel02 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel02.transform.position = new Vector3(-3.75f, 5.35f, 1f);
                                    jewel02.name = "jewel02";
                                    PoliceAndThief.jewel02 = jewel02;
                                    GameObject jewel03 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel03.transform.position = new Vector3(-7.7f, 11.3f, 1f);
                                    jewel03.name = "jewel03";
                                    PoliceAndThief.jewel03 = jewel03;
                                    GameObject jewel04 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel04.transform.position = new Vector3(-19.65f, 5.3f, 1f);
                                    jewel04.name = "jewel04";
                                    PoliceAndThief.jewel04 = jewel04;
                                    GameObject jewel05 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel05.transform.position = new Vector3(-19.65f, -8, 1f);
                                    jewel05.name = "jewel05";
                                    PoliceAndThief.jewel05 = jewel05;
                                    GameObject jewel06 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel06.transform.position = new Vector3(-5.45f, -13f, 1f);
                                    jewel06.name = "jewel06";
                                    PoliceAndThief.jewel06 = jewel06;
                                    GameObject jewel07 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel07.transform.position = new Vector3(-7.65f, -4.2f, 1f);
                                    jewel07.name = "jewel07";
                                    PoliceAndThief.jewel07 = jewel07;
                                    GameObject jewel08 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel08.transform.position = new Vector3(2f, -6.75f, 1f);
                                    jewel08.name = "jewel08";
                                    PoliceAndThief.jewel08 = jewel08;
                                    GameObject jewel09 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel09.transform.position = new Vector3(8.9f, 1.45f, 1f);
                                    jewel09.name = "jewel09";
                                    PoliceAndThief.jewel09 = jewel09;
                                    GameObject jewel10 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel10.transform.position = new Vector3(4.6f, -2.25f, 1f);
                                    jewel10.name = "jewel10";
                                    PoliceAndThief.jewel10 = jewel10;
                                    GameObject jewel11 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel11.transform.position = new Vector3(-5.05f, -0.88f, 1f);
                                    jewel11.name = "jewel11";
                                    PoliceAndThief.jewel11 = jewel11;
                                    GameObject jewel12 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel12.transform.position = new Vector3(-8.25f, -0.45f, 1f);
                                    jewel12.name = "jewel12";
                                    PoliceAndThief.jewel12 = jewel12;
                                    GameObject jewel13 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel13.transform.position = new Vector3(-19.75f, -1.55f, 1f);
                                    jewel13.name = "jewel13";
                                    PoliceAndThief.jewel13 = jewel13;
                                    GameObject jewel14 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel14.transform.position = new Vector3(-12.1f, -13.15f, 1f);
                                    jewel14.name = "jewel14";
                                    PoliceAndThief.jewel14 = jewel14;
                                    GameObject jewel15 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel15.transform.position = new Vector3(7.15f, -14.45f, 1f);
                                    jewel15.name = "jewel15";
                                    PoliceAndThief.jewel15 = jewel15;
                                    PoliceAndThief.thiefTreasures.Add(jewel01);
                                    PoliceAndThief.thiefTreasures.Add(jewel02);
                                    PoliceAndThief.thiefTreasures.Add(jewel03);
                                    PoliceAndThief.thiefTreasures.Add(jewel04);
                                    PoliceAndThief.thiefTreasures.Add(jewel05);
                                    PoliceAndThief.thiefTreasures.Add(jewel06);
                                    PoliceAndThief.thiefTreasures.Add(jewel07);
                                    PoliceAndThief.thiefTreasures.Add(jewel08);
                                    PoliceAndThief.thiefTreasures.Add(jewel09);
                                    PoliceAndThief.thiefTreasures.Add(jewel10);
                                    PoliceAndThief.thiefTreasures.Add(jewel11);
                                    PoliceAndThief.thiefTreasures.Add(jewel12);
                                    PoliceAndThief.thiefTreasures.Add(jewel13);
                                    PoliceAndThief.thiefTreasures.Add(jewel14);
                                    PoliceAndThief.thiefTreasures.Add(jewel15);
                                }
                            }
                            else {
                                foreach (PlayerControl player in PoliceAndThief.policeTeam) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(-10.2f, 1.18f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(-1.31f, -16.25f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                if (PlayerControl.LocalPlayer != null && !createdpoliceandthief) {
                                    GameObject cell = GameObject.Instantiate(CustomMain.customAssets.cell, PlayerControl.LocalPlayer.transform.parent);
                                    cell.name = "cell";
                                    cell.transform.position = new Vector3(-10.25f, 3.38f, 0.5f);
                                    cell.gameObject.layer = 9;
                                    cell.transform.GetChild(0).gameObject.layer = 9; 
                                    PoliceAndThief.cell = cell;
                                    GameObject cellbutton = GameObject.Instantiate(CustomMain.customAssets.freethiefbutton, PlayerControl.LocalPlayer.transform.parent);
                                    cellbutton.name = "cellbutton";
                                    cellbutton.transform.position = new Vector3(-10.2f, 0.93f, 0.5f);
                                    PoliceAndThief.cellbutton = cellbutton;
                                    GameObject jewelbutton = GameObject.Instantiate(CustomMain.customAssets.jewelbutton, PlayerControl.LocalPlayer.transform.parent);
                                    jewelbutton.name = "jewelbutton";
                                    jewelbutton.transform.position = new Vector3(0.20f, -17.15f, 0.5f);
                                    PoliceAndThief.jewelbutton = jewelbutton;
                                    GameObject thiefspaceship = GameObject.Instantiate(CustomMain.customAssets.thiefspaceship, PlayerControl.LocalPlayer.transform.parent);
                                    thiefspaceship.name = "thiefspaceship";
                                    thiefspaceship.transform.position = new Vector3(1.765f, -19.16f, 0.6f);
                                    GameObject thiefspaceshiphatch = GameObject.Instantiate(CustomMain.customAssets.thiefspaceshiphatch, PlayerControl.LocalPlayer.transform.parent);
                                    thiefspaceshiphatch.name = "thiefspaceshiphatch";
                                    thiefspaceshiphatch.transform.position = new Vector3(1.765f, -19.16f, 0.6f);
                                    createdpoliceandthief = true;

                                    // Remove camera use and admin table on Skeld
                                    GameObject cameraStand = GameObject.Find("SurvConsole");
                                    cameraStand.GetComponent<PolygonCollider2D>().enabled = false;
                                    GameObject admin = GameObject.Find("MapRoomConsole");
                                    admin.GetComponent<CircleCollider2D>().enabled = false;

                                    // Spawn jewels
                                    GameObject jewel01 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel01.transform.position = new Vector3(-18.65f, -9.9f, 1f);
                                    jewel01.name = "jewel01";
                                    PoliceAndThief.jewel01 = jewel01;
                                    GameObject jewel02 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel02.transform.position = new Vector3(-21.5f, -2, 1f);
                                    jewel02.name = "jewel02";
                                    PoliceAndThief.jewel02 = jewel02;
                                    GameObject jewel03 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel03.transform.position = new Vector3(-5.9f, -8.25f, 1f);
                                    jewel03.name = "jewel03";
                                    PoliceAndThief.jewel03 = jewel03;
                                    GameObject jewel04 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel04.transform.position = new Vector3(4.5f, -7.5f, 1f);
                                    jewel04.name = "jewel04";
                                    PoliceAndThief.jewel04 = jewel04;
                                    GameObject jewel05 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel05.transform.position = new Vector3(7.85f, -14.45f, 1f);
                                    jewel05.name = "jewel05";
                                    PoliceAndThief.jewel05 = jewel05;
                                    GameObject jewel06 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel06.transform.position = new Vector3(6.65f, -4.8f, 1f);
                                    jewel06.name = "jewel06";
                                    PoliceAndThief.jewel06 = jewel06;
                                    GameObject jewel07 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel07.transform.position = new Vector3(10.5f, 2.15f, 1f);
                                    jewel07.name = "jewel07";
                                    PoliceAndThief.jewel07 = jewel07;
                                    GameObject jewel08 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                    jewel08.transform.position = new Vector3(-5.5f, 3.5f, 1f);
                                    jewel08.name = "jewel08";
                                    PoliceAndThief.jewel08 = jewel08;
                                    GameObject jewel09 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel09.transform.position = new Vector3(-19, -1.2f, 1f);
                                    jewel09.name = "jewel09";
                                    PoliceAndThief.jewel09 = jewel09;
                                    GameObject jewel10 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel10.transform.position = new Vector3(-21.5f, -8.35f, 1f);
                                    jewel10.name = "jewel10";
                                    PoliceAndThief.jewel10 = jewel10;
                                    GameObject jewel11 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel11.transform.position = new Vector3(-12.5f, -3.75f, 1f);
                                    jewel11.name = "jewel11";
                                    PoliceAndThief.jewel11 = jewel11;
                                    GameObject jewel12 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel12.transform.position = new Vector3(-5.9f, -5.25f, 1f);
                                    jewel12.name = "jewel12";
                                    PoliceAndThief.jewel12 = jewel12;
                                    GameObject jewel13 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel13.transform.position = new Vector3(2.65f, -16.5f, 1f);
                                    jewel13.name = "jewel13";
                                    PoliceAndThief.jewel13 = jewel13;
                                    GameObject jewel14 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel14.transform.position = new Vector3(16.75f, -4.75f, 1f);
                                    jewel14.name = "jewel14";
                                    PoliceAndThief.jewel14 = jewel14;
                                    GameObject jewel15 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                    jewel15.transform.position = new Vector3(3.8f, 3.5f, 1f);
                                    jewel15.name = "jewel15";
                                    PoliceAndThief.jewel15 = jewel15;
                                    PoliceAndThief.thiefTreasures.Add(jewel01);
                                    PoliceAndThief.thiefTreasures.Add(jewel02);
                                    PoliceAndThief.thiefTreasures.Add(jewel03);
                                    PoliceAndThief.thiefTreasures.Add(jewel04);
                                    PoliceAndThief.thiefTreasures.Add(jewel05);
                                    PoliceAndThief.thiefTreasures.Add(jewel06);
                                    PoliceAndThief.thiefTreasures.Add(jewel07);
                                    PoliceAndThief.thiefTreasures.Add(jewel08);
                                    PoliceAndThief.thiefTreasures.Add(jewel09);
                                    PoliceAndThief.thiefTreasures.Add(jewel10);
                                    PoliceAndThief.thiefTreasures.Add(jewel11);
                                    PoliceAndThief.thiefTreasures.Add(jewel12);
                                    PoliceAndThief.thiefTreasures.Add(jewel13);
                                    PoliceAndThief.thiefTreasures.Add(jewel14);
                                    PoliceAndThief.thiefTreasures.Add(jewel15);
                                }
                            }
                            break;
                        // MiraHQ
                        case 1:
                            foreach (PlayerControl player in PoliceAndThief.policeTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(1.8f, -1f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(17.75f, 11.5f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdpoliceandthief) {
                                GameObject cell = GameObject.Instantiate(CustomMain.customAssets.cell, PlayerControl.LocalPlayer.transform.parent);
                                cell.name = "cell";
                                cell.transform.position = new Vector3(1.75f, 1.125f, 0.5f);
                                cell.gameObject.layer = 9;
                                cell.transform.GetChild(0).gameObject.layer = 9; 
                                PoliceAndThief.cell = cell;
                                GameObject cellbutton = GameObject.Instantiate(CustomMain.customAssets.freethiefbutton, PlayerControl.LocalPlayer.transform.parent);
                                cellbutton.name = "cellbutton";
                                cellbutton.transform.position = new Vector3(1.8f, -1.25f, 0.5f);
                                PoliceAndThief.cellbutton = cellbutton;
                                GameObject jewelbutton = GameObject.Instantiate(CustomMain.customAssets.jewelbutton, PlayerControl.LocalPlayer.transform.parent);
                                jewelbutton.name = "jewelbutton";
                                jewelbutton.transform.position = new Vector3(18.5f, 13.85f, 0.5f);
                                PoliceAndThief.jewelbutton = jewelbutton;
                                GameObject thiefspaceship = GameObject.Instantiate(CustomMain.customAssets.thiefspaceship, PlayerControl.LocalPlayer.transform.parent);
                                thiefspaceship.name = "thiefspaceship";
                                thiefspaceship.transform.position = new Vector3(21.4f, 14.2f, 0.6f);
                                createdpoliceandthief = true;

                                // Remove Doorlog use, Decontamintion doors and admin table on MiraHQ
                                GameObject DoorLog = GameObject.Find("SurvLogConsole");
                                DoorLog.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject deconUpperDoor = GameObject.Find("UpperDoor");
                                deconUpperDoor.SetActive(false);
                                GameObject deconLowerDoor = GameObject.Find("LowerDoor");
                                deconLowerDoor.SetActive(false);
                                GameObject deconUpperDoorPanelTop = GameObject.Find("DeconDoorPanel-Top");
                                deconUpperDoorPanelTop.SetActive(false);
                                GameObject deconUpperDoorPanelHigh = GameObject.Find("DeconDoorPanel-High");
                                deconUpperDoorPanelHigh.SetActive(false);
                                GameObject deconUpperDoorPanelBottom = GameObject.Find("DeconDoorPanel-Bottom");
                                deconUpperDoorPanelBottom.SetActive(false);
                                GameObject deconUpperDoorPanelLow = GameObject.Find("DeconDoorPanel-Low");
                                deconUpperDoorPanelLow.SetActive(false);
                                GameObject admin = GameObject.Find("AdminMapConsole");
                                admin.GetComponent<CircleCollider2D>().enabled = false;

                                // Spawn jewels
                                GameObject jewel01 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel01.transform.position = new Vector3(-4.5f, 2.5f, 1f);
                                jewel01.name = "jewel01";
                                PoliceAndThief.jewel01 = jewel01;
                                GameObject jewel02 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel02.transform.position = new Vector3(6.25f, 14f, 1f);
                                jewel02.name = "jewel02";
                                PoliceAndThief.jewel02 = jewel02;
                                GameObject jewel03 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel03.transform.position = new Vector3(9.15f, 4.75f, 1f);
                                jewel03.name = "jewel03";
                                PoliceAndThief.jewel03 = jewel03;
                                GameObject jewel04 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel04.transform.position = new Vector3(14.75f, 20.5f, 1f);
                                jewel04.name = "jewel04";
                                PoliceAndThief.jewel04 = jewel04;
                                GameObject jewel05 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel05.transform.position = new Vector3(19.5f, 17.5f, 1f);
                                jewel05.name = "jewel05";
                                PoliceAndThief.jewel05 = jewel05;
                                GameObject jewel06 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel06.transform.position = new Vector3(21, 24.1f, 1f);
                                jewel06.name = "jewel06";
                                PoliceAndThief.jewel06 = jewel06;
                                GameObject jewel07 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel07.transform.position = new Vector3(19.5f, 4.75f, 1f);
                                jewel07.name = "jewel07";
                                PoliceAndThief.jewel07 = jewel07;
                                GameObject jewel08 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel08.transform.position = new Vector3(28.25f, 0, 1f);
                                jewel08.name = "jewel08";
                                PoliceAndThief.jewel08 = jewel08;
                                GameObject jewel09 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel09.transform.position = new Vector3(2.45f, 11.25f, 1f);
                                jewel09.name = "jewel09";
                                PoliceAndThief.jewel09 = jewel09;
                                GameObject jewel10 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel10.transform.position = new Vector3(4.4f, 1.75f, 1f);
                                jewel10.name = "jewel10";
                                PoliceAndThief.jewel10 = jewel10;
                                GameObject jewel11 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel11.transform.position = new Vector3(9.25f, 13f, 1f);
                                jewel11.name = "jewel11";
                                PoliceAndThief.jewel11 = jewel11;
                                GameObject jewel12 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel12.transform.position = new Vector3(13.75f, 23.5f, 1f);
                                jewel12.name = "jewel12";
                                PoliceAndThief.jewel12 = jewel12;
                                GameObject jewel13 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel13.transform.position = new Vector3(16, 4, 1f);
                                jewel13.name = "jewel13";
                                PoliceAndThief.jewel13 = jewel13;
                                GameObject jewel14 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel14.transform.position = new Vector3(15.35f, -0.9f, 1f);
                                jewel14.name = "jewel14";
                                PoliceAndThief.jewel14 = jewel14;
                                GameObject jewel15 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel15.transform.position = new Vector3(19.5f, -1.75f, 1f);
                                jewel15.name = "jewel15";
                                PoliceAndThief.jewel15 = jewel15;
                                PoliceAndThief.thiefTreasures.Add(jewel01);
                                PoliceAndThief.thiefTreasures.Add(jewel02);
                                PoliceAndThief.thiefTreasures.Add(jewel03);
                                PoliceAndThief.thiefTreasures.Add(jewel04);
                                PoliceAndThief.thiefTreasures.Add(jewel05);
                                PoliceAndThief.thiefTreasures.Add(jewel06);
                                PoliceAndThief.thiefTreasures.Add(jewel07);
                                PoliceAndThief.thiefTreasures.Add(jewel08);
                                PoliceAndThief.thiefTreasures.Add(jewel09);
                                PoliceAndThief.thiefTreasures.Add(jewel10);
                                PoliceAndThief.thiefTreasures.Add(jewel11);
                                PoliceAndThief.thiefTreasures.Add(jewel12);
                                PoliceAndThief.thiefTreasures.Add(jewel13);
                                PoliceAndThief.thiefTreasures.Add(jewel14);
                                PoliceAndThief.thiefTreasures.Add(jewel15);
                            }
                            break;
                        // Polus
                        case 2:
                            foreach (PlayerControl player in PoliceAndThief.policeTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(8.18f, -7.4f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(30f, -15.75f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdpoliceandthief) {
                                GameObject cell = GameObject.Instantiate(CustomMain.customAssets.cell, PlayerControl.LocalPlayer.transform.parent);
                                cell.name = "cell";
                                cell.transform.position = new Vector3(8.25f, -5.15f, 0.5f);
                                cell.gameObject.layer = 9;
                                cell.transform.GetChild(0).gameObject.layer = 9; 
                                PoliceAndThief.cell = cell;
                                GameObject cellbutton = GameObject.Instantiate(CustomMain.customAssets.freethiefbutton, PlayerControl.LocalPlayer.transform.parent);
                                cellbutton.name = "cellbutton";
                                cellbutton.transform.position = new Vector3(8.2f, -7.5f, 0.5f);
                                PoliceAndThief.cellbutton = cellbutton;
                                GameObject jewelbutton = GameObject.Instantiate(CustomMain.customAssets.jewelbutton, PlayerControl.LocalPlayer.transform.parent);
                                jewelbutton.name = "jewelbutton";
                                jewelbutton.transform.position = new Vector3(32.25f, -15.9f, 0.5f);
                                PoliceAndThief.jewelbutton = jewelbutton;
                                GameObject thiefspaceship = GameObject.Instantiate(CustomMain.customAssets.thiefspaceship, PlayerControl.LocalPlayer.transform.parent);
                                thiefspaceship.name = "thiefspaceship";
                                thiefspaceship.transform.position = new Vector3(35.35f, -15.55f, 0.8f);
                                createdpoliceandthief = true;

                                // Remove Decon doors, camera use, vitals, admin tables on Polus
                                GameObject lowerdecon = GameObject.Find("LowerDecon");
                                lowerdecon.SetActive(false);
                                GameObject upperdecon = GameObject.Find("UpperDecon");
                                upperdecon.SetActive(false);
                                GameObject survCameras = GameObject.Find("Surv_Panel");
                                survCameras.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject vitals = GameObject.Find("panel_vitals");
                                vitals.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject adminone = GameObject.Find("panel_map");
                                adminone.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject admintwo = GameObject.Find("panel_map (1)");
                                admintwo.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject prisonVent = GameObject.Find("ElectricBuildingVent");
                                prisonVent.transform.position = new Vector3(11.75f, -7.75f, prisonVent.transform.position.z);
                                GameObject ramp = GameObject.Find("ramp");
                                ramp.transform.position = new Vector3(ramp.transform.position.x, ramp.transform.position.y, 0.75f);
                                GameObject bathroomVent = GameObject.Find("BathroomVent");
                                bathroomVent.transform.position = new Vector3(34, -10.3f, bathroomVent.transform.position.z);

                                // Spawn jewels
                                GameObject jewel01 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel01.transform.position = new Vector3(16.7f, -2.65f, 0.75f);
                                jewel01.name = "jewel01";
                                PoliceAndThief.jewel01 = jewel01;
                                GameObject jewel02 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel02.transform.position = new Vector3(25.35f, -7.35f, 0.75f);
                                jewel02.name = "jewel02";
                                PoliceAndThief.jewel02 = jewel02;
                                GameObject jewel03 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel03.transform.position = new Vector3(34.9f, -9.75f, 0.75f);
                                jewel03.name = "jewel03";
                                PoliceAndThief.jewel03 = jewel03;
                                GameObject jewel04 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel04.transform.position = new Vector3(36.5f, -21.75f, 0.75f);
                                jewel04.name = "jewel04";
                                PoliceAndThief.jewel04 = jewel04;
                                GameObject jewel05 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel05.transform.position = new Vector3(17.25f, -17.5f, 0.75f);
                                jewel05.name = "jewel05";
                                PoliceAndThief.jewel05 = jewel05;
                                GameObject jewel06 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel06.transform.position = new Vector3(10.9f, -20.5f, -0.75f);
                                jewel06.name = "jewel06";
                                PoliceAndThief.jewel06 = jewel06;
                                GameObject jewel07 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel07.transform.position = new Vector3(1.5f, -20.25f, 0.75f);
                                jewel07.name = "jewel07";
                                PoliceAndThief.jewel07 = jewel07;
                                GameObject jewel08 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel08.transform.position = new Vector3(3f, -12f, 0.75f);
                                jewel08.name = "jewel08";
                                PoliceAndThief.jewel08 = jewel08;
                                GameObject jewel09 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel09.transform.position = new Vector3(30f, -7.35f, 0.75f);
                                jewel09.name = "jewel09";
                                PoliceAndThief.jewel09 = jewel09;
                                GameObject jewel10 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel10.transform.position = new Vector3(40.25f, -8f, 0.75f);
                                jewel10.name = "jewel10";
                                PoliceAndThief.jewel10 = jewel10;
                                GameObject jewel11 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel11.transform.position = new Vector3(26f, -17.15f, 0.75f);
                                jewel11.name = "jewel11";
                                PoliceAndThief.jewel11 = jewel11;
                                GameObject jewel12 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel12.transform.position = new Vector3(22f, -25.25f, 0.75f);
                                jewel12.name = "jewel12";
                                PoliceAndThief.jewel12 = jewel12;
                                GameObject jewel13 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel13.transform.position = new Vector3(20.65f, -12f, 0.75f);
                                jewel13.name = "jewel13";
                                PoliceAndThief.jewel13 = jewel13;
                                GameObject jewel14 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel14.transform.position = new Vector3(9.75f, -12.25f, 0.75f);
                                jewel14.name = "jewel14";
                                PoliceAndThief.jewel14 = jewel14;
                                GameObject jewel15 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel15.transform.position = new Vector3(2.25f, -24f, 0.75f);
                                jewel15.name = "jewel15";
                                PoliceAndThief.jewel15 = jewel15;
                                PoliceAndThief.thiefTreasures.Add(jewel01);
                                PoliceAndThief.thiefTreasures.Add(jewel02);
                                PoliceAndThief.thiefTreasures.Add(jewel03);
                                PoliceAndThief.thiefTreasures.Add(jewel04);
                                PoliceAndThief.thiefTreasures.Add(jewel05);
                                PoliceAndThief.thiefTreasures.Add(jewel06);
                                PoliceAndThief.thiefTreasures.Add(jewel07);
                                PoliceAndThief.thiefTreasures.Add(jewel08);
                                PoliceAndThief.thiefTreasures.Add(jewel09);
                                PoliceAndThief.thiefTreasures.Add(jewel10);
                                PoliceAndThief.thiefTreasures.Add(jewel11);
                                PoliceAndThief.thiefTreasures.Add(jewel12);
                                PoliceAndThief.thiefTreasures.Add(jewel13);
                                PoliceAndThief.thiefTreasures.Add(jewel14);
                                PoliceAndThief.thiefTreasures.Add(jewel15);
                            }
                            break;
                        // Dleks
                        case 3:
                            foreach (PlayerControl player in PoliceAndThief.policeTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(10.2f, 1.18f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(1.31f, -16.25f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdpoliceandthief) {
                                GameObject cell = GameObject.Instantiate(CustomMain.customAssets.cell, PlayerControl.LocalPlayer.transform.parent);
                                cell.name = "cell";
                                cell.transform.position = new Vector3(10.25f, 3.38f, 0.5f);
                                cell.gameObject.layer = 9;
                                cell.transform.GetChild(0).gameObject.layer = 9; 
                                PoliceAndThief.cell = cell;
                                GameObject cellbutton = GameObject.Instantiate(CustomMain.customAssets.freethiefbutton, PlayerControl.LocalPlayer.transform.parent);
                                cellbutton.name = "cellbutton";
                                cellbutton.transform.position = new Vector3(10.2f, 0.93f, 0.5f);
                                PoliceAndThief.cellbutton = cellbutton;
                                GameObject jewelbutton = GameObject.Instantiate(CustomMain.customAssets.jewelbutton, PlayerControl.LocalPlayer.transform.parent);
                                jewelbutton.name = "jewelbutton";
                                jewelbutton.transform.position = new Vector3(-0.20f, -17.15f, 0.5f);
                                PoliceAndThief.jewelbutton = jewelbutton;
                                GameObject thiefspaceship = GameObject.Instantiate(CustomMain.customAssets.thiefspaceship, PlayerControl.LocalPlayer.transform.parent);
                                thiefspaceship.name = "thiefspaceship";
                                thiefspaceship.transform.position = new Vector3(1.345f, -19.16f, 0.6f);
                                GameObject thiefspaceshiphatch = GameObject.Instantiate(CustomMain.customAssets.thiefspaceshiphatch, PlayerControl.LocalPlayer.transform.parent);
                                thiefspaceshiphatch.name = "thiefspaceshiphatch";
                                thiefspaceshiphatch.transform.position = new Vector3(1.345f, -19.16f, 0.6f);
                                createdpoliceandthief = true;

                                // Remove camera use and admin table on Skeld
                                GameObject cameraStand = GameObject.Find("SurvConsole");
                                cameraStand.GetComponent<PolygonCollider2D>().enabled = false;
                                GameObject admin = GameObject.Find("MapRoomConsole");
                                admin.GetComponent<CircleCollider2D>().enabled = false;

                                // Spawn jewels
                                GameObject jewel01 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel01.transform.position = new Vector3(18.65f, -9.9f, 1f);
                                jewel01.name = "jewel01";
                                PoliceAndThief.jewel01 = jewel01;
                                GameObject jewel02 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel02.transform.position = new Vector3(21.5f, -2, 1f);
                                jewel02.name = "jewel02";
                                PoliceAndThief.jewel02 = jewel02;
                                GameObject jewel03 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel03.transform.position = new Vector3(5.9f, -8.25f, 1f);
                                jewel03.name = "jewel03";
                                PoliceAndThief.jewel03 = jewel03;
                                GameObject jewel04 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel04.transform.position = new Vector3(-4.5f, -7.5f, 1f);
                                jewel04.name = "jewel04";
                                PoliceAndThief.jewel04 = jewel04;
                                GameObject jewel05 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel05.transform.position = new Vector3(-7.85f, -14.45f, 1f);
                                jewel05.name = "jewel05";
                                PoliceAndThief.jewel05 = jewel05;
                                GameObject jewel06 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel06.transform.position = new Vector3(-6.65f, -4.8f, 1f);
                                jewel06.name = "jewel06";
                                PoliceAndThief.jewel06 = jewel06;
                                GameObject jewel07 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel07.transform.position = new Vector3(-10.5f, 2.15f, 1f);
                                jewel07.name = "jewel07";
                                PoliceAndThief.jewel07 = jewel07;
                                GameObject jewel08 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel08.transform.position = new Vector3(5.5f, 3.5f, 1f);
                                jewel08.name = "jewel08";
                                PoliceAndThief.jewel08 = jewel08;
                                GameObject jewel09 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel09.transform.position = new Vector3(19, -1.2f, 1f);
                                jewel09.name = "jewel09";
                                PoliceAndThief.jewel09 = jewel09;
                                GameObject jewel10 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel10.transform.position = new Vector3(21.5f, -8.35f, 1f);
                                jewel10.name = "jewel10";
                                PoliceAndThief.jewel10 = jewel10;
                                GameObject jewel11 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel11.transform.position = new Vector3(12.5f, -3.75f, 1f);
                                jewel11.name = "jewel11";
                                PoliceAndThief.jewel11 = jewel11;
                                GameObject jewel12 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel12.transform.position = new Vector3(5.9f, -5.25f, 1f);
                                jewel12.name = "jewel12";
                                PoliceAndThief.jewel12 = jewel12;
                                GameObject jewel13 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel13.transform.position = new Vector3(-2.65f, -16.5f, 1f);
                                jewel13.name = "jewel13";
                                PoliceAndThief.jewel13 = jewel13;
                                GameObject jewel14 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel14.transform.position = new Vector3(-16.75f, -4.75f, 1f);
                                jewel14.name = "jewel14";
                                PoliceAndThief.jewel14 = jewel14;
                                GameObject jewel15 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel15.transform.position = new Vector3(-3.8f, 3.5f, 1f);
                                jewel15.name = "jewel15";
                                PoliceAndThief.jewel15 = jewel15;
                                PoliceAndThief.thiefTreasures.Add(jewel01);
                                PoliceAndThief.thiefTreasures.Add(jewel02);
                                PoliceAndThief.thiefTreasures.Add(jewel03);
                                PoliceAndThief.thiefTreasures.Add(jewel04);
                                PoliceAndThief.thiefTreasures.Add(jewel05);
                                PoliceAndThief.thiefTreasures.Add(jewel06);
                                PoliceAndThief.thiefTreasures.Add(jewel07);
                                PoliceAndThief.thiefTreasures.Add(jewel08);
                                PoliceAndThief.thiefTreasures.Add(jewel09);
                                PoliceAndThief.thiefTreasures.Add(jewel10);
                                PoliceAndThief.thiefTreasures.Add(jewel11);
                                PoliceAndThief.thiefTreasures.Add(jewel12);
                                PoliceAndThief.thiefTreasures.Add(jewel13);
                                PoliceAndThief.thiefTreasures.Add(jewel14);
                                PoliceAndThief.thiefTreasures.Add(jewel15);
                            }
                            break;
                        // Airship
                        case 4:
                            foreach (PlayerControl player in PoliceAndThief.policeTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(-18.5f, 0.75f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(7.15f, -14.5f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdpoliceandthief) {
                                GameObject cell = GameObject.Instantiate(CustomMain.customAssets.cell, PlayerControl.LocalPlayer.transform.parent);
                                cell.name = "cell";
                                cell.transform.position = new Vector3(-18.45f, 3.55f, 0.5f);
                                cell.gameObject.layer = 9;
                                cell.transform.GetChild(0).gameObject.layer = 9; 
                                PoliceAndThief.cell = cell;
                                GameObject cellbutton = GameObject.Instantiate(CustomMain.customAssets.freethiefbutton, PlayerControl.LocalPlayer.transform.parent);
                                cellbutton.name = "cellbutton";
                                cellbutton.transform.position = new Vector3(-18.5f, 0.5f, 0.5f);
                                PoliceAndThief.cellbutton = cellbutton;
                                GameObject jewelbutton = GameObject.Instantiate(CustomMain.customAssets.jewelbutton, PlayerControl.LocalPlayer.transform.parent);
                                jewelbutton.name = "jewelbutton";
                                jewelbutton.transform.position = new Vector3(10.275f, -16.3f, -0.01f);
                                PoliceAndThief.jewelbutton = jewelbutton;
                                GameObject thiefspaceship = GameObject.Instantiate(CustomMain.customAssets.thiefspaceship, PlayerControl.LocalPlayer.transform.parent);
                                thiefspaceship.name = "thiefspaceship";
                                thiefspaceship.transform.position = new Vector3(13.5f, -16f, 0.6f);
                                createdpoliceandthief = true;

                                // Remove camera use, admin table, vitals, electrical doors on Airship
                                GameObject cameras = GameObject.Find("task_cams");
                                cameras.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject admin = GameObject.Find("panel_cockpit_map");
                                admin.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject vitals = GameObject.Find("panel_vitals");
                                vitals.GetComponent<CircleCollider2D>().enabled = false;
                                GameObject LeftDoorTop = GameObject.Find("LeftDoorTop");
                                LeftDoorTop.SetActive(false);
                                GameObject TopLeftVert = GameObject.Find("TopLeftVert");
                                TopLeftVert.SetActive(false);
                                GameObject TopLeftHort = GameObject.Find("TopLeftHort");
                                TopLeftHort.SetActive(false);
                                GameObject BottomHort = GameObject.Find("BottomHort");
                                BottomHort.SetActive(false);
                                GameObject TopCenterHort = GameObject.Find("TopCenterHort");
                                TopCenterHort.SetActive(false);
                                GameObject LeftVert = GameObject.Find("LeftVert");
                                LeftVert.SetActive(false);
                                GameObject RightVert = GameObject.Find("RightVert");
                                RightVert.SetActive(false);
                                GameObject TopRightVert = GameObject.Find("TopRightVert");
                                TopRightVert.SetActive(false);
                                GameObject TopRightHort = GameObject.Find("TopRightHort");
                                TopRightHort.SetActive(false);
                                GameObject BottomRightHort = GameObject.Find("BottomRightHort");
                                BottomRightHort.SetActive(false);
                                GameObject BottomRightVert = GameObject.Find("BottomRightVert");
                                BottomRightVert.SetActive(false);
                                GameObject LeftDoorBottom = GameObject.Find("LeftDoorBottom");
                                LeftDoorBottom.SetActive(false);
                                GameObject laddermeeting = GameObject.Find("ladder_meeting");
                                laddermeeting.SetActive(false);
                                GameObject platform = GameObject.Find("Platform");
                                platform.SetActive(false);
                                GameObject platformleft = GameObject.Find("PlatformLeft");
                                platformleft.SetActive(false);
                                GameObject platformright = GameObject.Find("PlatformRight");
                                platformright.SetActive(false);

                                // Spawn jewels
                                GameObject jewel01 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel01.transform.position = new Vector3(-23.5f, -1.5f, 1f);
                                jewel01.name = "jewel01";
                                PoliceAndThief.jewel01 = jewel01;
                                GameObject jewel02 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel02.transform.position = new Vector3(-14.15f, -4.85f, 1f);
                                jewel02.name = "jewel02";
                                PoliceAndThief.jewel02 = jewel02;
                                GameObject jewel03 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel03.transform.position = new Vector3(-13.9f, -16.25f, 1f);
                                jewel03.name = "jewel03";
                                PoliceAndThief.jewel03 = jewel03;
                                GameObject jewel04 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel04.transform.position = new Vector3(-0.85f, -2.5f, 1f);
                                jewel04.name = "jewel04";
                                PoliceAndThief.jewel04 = jewel04;
                                GameObject jewel05 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel05.transform.position = new Vector3(-5, 8.5f, 1f);
                                jewel05.name = "jewel05";
                                PoliceAndThief.jewel05 = jewel05;
                                GameObject jewel06 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel06.transform.position = new Vector3(19.3f, -4.15f, 1f);
                                jewel06.name = "jewel06";
                                PoliceAndThief.jewel06 = jewel06;
                                GameObject jewel07 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel07.transform.position = new Vector3(19.85f, 8, 1f);
                                jewel07.name = "jewel07";
                                PoliceAndThief.jewel07 = jewel07;
                                GameObject jewel08 = GameObject.Instantiate(CustomMain.customAssets.jeweldiamond, PlayerControl.LocalPlayer.transform.parent);
                                jewel08.transform.position = new Vector3(28.85f, -1.75f, 1f);
                                jewel08.name = "jewel08";
                                PoliceAndThief.jewel08 = jewel08;
                                GameObject jewel09 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel09.transform.position = new Vector3(-14.5f, -8.5f, 1f);
                                jewel09.name = "jewel09";
                                PoliceAndThief.jewel09 = jewel09;
                                GameObject jewel10 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel10.transform.position = new Vector3(6.3f, -2.75f, 1f);
                                jewel10.name = "jewel10";
                                PoliceAndThief.jewel10 = jewel10;
                                GameObject jewel11 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel11.transform.position = new Vector3(20.75f, 2.5f, 1f);
                                jewel11.name = "jewel11";
                                PoliceAndThief.jewel11 = jewel11;
                                GameObject jewel12 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel12.transform.position = new Vector3(29.25f, 7, 1f);
                                jewel12.name = "jewel12";
                                PoliceAndThief.jewel12 = jewel12;
                                GameObject jewel13 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel13.transform.position = new Vector3(37.5f, -3.5f, 1f);
                                jewel13.name = "jewel13";
                                PoliceAndThief.jewel13 = jewel13;
                                GameObject jewel14 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel14.transform.position = new Vector3(25.2f, -8.75f, 1f);
                                jewel14.name = "jewel14";
                                PoliceAndThief.jewel14 = jewel14;
                                GameObject jewel15 = GameObject.Instantiate(CustomMain.customAssets.jewelruby, PlayerControl.LocalPlayer.transform.parent);
                                jewel15.transform.position = new Vector3(16.3f, -11, 1f);
                                jewel15.name = "jewel15";
                                PoliceAndThief.jewel15 = jewel15;
                                PoliceAndThief.thiefTreasures.Add(jewel01);
                                PoliceAndThief.thiefTreasures.Add(jewel02);
                                PoliceAndThief.thiefTreasures.Add(jewel03);
                                PoliceAndThief.thiefTreasures.Add(jewel04);
                                PoliceAndThief.thiefTreasures.Add(jewel05);
                                PoliceAndThief.thiefTreasures.Add(jewel06);
                                PoliceAndThief.thiefTreasures.Add(jewel07);
                                PoliceAndThief.thiefTreasures.Add(jewel08);
                                PoliceAndThief.thiefTreasures.Add(jewel09);
                                PoliceAndThief.thiefTreasures.Add(jewel10);
                                PoliceAndThief.thiefTreasures.Add(jewel11);
                                PoliceAndThief.thiefTreasures.Add(jewel12);
                                PoliceAndThief.thiefTreasures.Add(jewel13);
                                PoliceAndThief.thiefTreasures.Add(jewel14);
                                PoliceAndThief.thiefTreasures.Add(jewel15);
                            }
                            break;
                    }

                    if (!PoliceAndThief.policeCanSeeJewels) {
                        foreach (PlayerControl police in PoliceAndThief.policeTeam) {
                            if (police == PlayerControl.LocalPlayer) {
                                foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                                    jewel.SetActive(false);
                                }
                            }
                        }
                    }
                    new CustomMessage("Time Left: ", PoliceAndThief.matchDuration, -1, -1.3f, 6);
                    PoliceAndThief.thiefpointCounter = "Stolen Jewels: " + "<color=#00F7FFFF>" + PoliceAndThief.currentJewelsStoled + "/" + PoliceAndThief.requiredJewels + "</color> | " + "Captured Thiefs: " + "<color=#928B55FF>" + PoliceAndThief.currentThiefsCaptured + "/" + PoliceAndThief.thiefTeam.Count + "</color>";
                    new CustomMessage(PoliceAndThief.thiefpointCounter, PoliceAndThief.matchDuration, -1, 1.9f, 8);
                }// King of the hill
                else if (KingOfTheHill.kingOfTheHillMode && !PoliceAndThief.policeAndThiefMode && !CaptureTheFlag.captureTheFlagMode) {
                    switch (PlayerControl.GameOptions.MapId) {
                        // Skeld
                        case 0:
                            if (activatedSensei) {
                                if (PlayerControl.LocalPlayer == KingOfTheHill.usurperPlayer) {
                                    KingOfTheHill.usurperPlayer.transform.position = new Vector3(-6.8f, 10.75f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(KingOfTheHill.usurperPlayer);
                                }
                                
                                foreach (PlayerControl player in KingOfTheHill.greenTeam) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(-16.4f, -10.25f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                foreach (PlayerControl player in KingOfTheHill.yellowTeam) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(7f, -14.15f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                if (PlayerControl.LocalPlayer != null && !createdkingofthehill) {
                                    GameObject greenteamfloor = GameObject.Instantiate(CustomMain.customAssets.greenfloor, PlayerControl.LocalPlayer.transform.parent);
                                    greenteamfloor.name = "greenteamfloor";
                                    greenteamfloor.transform.position = new Vector3(-16.4f, -10.5f, 0.5f);
                                    GameObject yellowteamfloor = GameObject.Instantiate(CustomMain.customAssets.yellowfloor, PlayerControl.LocalPlayer.transform.parent);
                                    yellowteamfloor.name = "yellowteamfloor";
                                    yellowteamfloor.transform.position = new Vector3(7f, -14.4f, 0.5f); 
                                    GameObject greenkingaura = GameObject.Instantiate(CustomMain.customAssets.greenaura, KingOfTheHill.greenKingplayer.transform);
                                    greenkingaura.name = "greenkingaura";
                                    greenkingaura.transform.position = new Vector3(KingOfTheHill.greenKingplayer.transform.position.x, KingOfTheHill.greenKingplayer.transform.position.y, 0.4f);
                                    KingOfTheHill.greenkingaura = greenkingaura;
                                    GameObject yellowkingaura = GameObject.Instantiate(CustomMain.customAssets.yellowaura, KingOfTheHill.yellowKingplayer.transform);
                                    yellowkingaura.name = "yellowkingaura";
                                    yellowkingaura.transform.position = new Vector3(KingOfTheHill.yellowKingplayer.transform.position.x, KingOfTheHill.yellowKingplayer.transform.position.y, 0.4f);
                                    KingOfTheHill.yellowkingaura = yellowkingaura;
                                    GameObject flagzoneone = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                    flagzoneone.name = "flagzoneone";
                                    flagzoneone.transform.position = new Vector3(7.85f, -1.5f, 0.4f);
                                    KingOfTheHill.flagzoneone = flagzoneone;
                                    GameObject zoneone = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                    zoneone.name = "zoneone";
                                    zoneone.transform.position = new Vector3(7.85f, -1.5f, 0.5f);
                                    KingOfTheHill.zoneone = zoneone;
                                    GameObject flagzonetwo = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                    flagzonetwo.name = "flagzonetwo";
                                    flagzonetwo.transform.position = new Vector3(-6.35f, -1.1f, 0.4f);
                                    KingOfTheHill.flagzonetwo = flagzonetwo;
                                    GameObject zonetwo = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                    zonetwo.name = "zonetwo";
                                    zonetwo.transform.position = new Vector3(-6.35f, -1.1f, 0.5f);
                                    KingOfTheHill.zonetwo = zonetwo;
                                    GameObject flagzonethree = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                    flagzonethree.name = "flagzonethree";
                                    flagzonethree.transform.position = new Vector3(-12.15f, 7.35f, 0.4f);
                                    KingOfTheHill.flagzonethree = flagzonethree;
                                    GameObject zonethree = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                    zonethree.name = "zonethree";
                                    zonethree.transform.position = new Vector3(-12.15f, 7.35f, 0.5f);
                                    KingOfTheHill.zonethree = zonethree;
                                    KingOfTheHill.kingZones.Add(zoneone);
                                    KingOfTheHill.kingZones.Add(zonetwo);
                                    KingOfTheHill.kingZones.Add(zonethree);
                                    createdkingofthehill = true;

                                    // Remove camera use and admin table on Skeld
                                    GameObject cameraStand = GameObject.Find("SurvConsole");
                                    cameraStand.GetComponent<PolygonCollider2D>().enabled = false;
                                    GameObject admin = GameObject.Find("MapRoomConsole");
                                    admin.GetComponent<CircleCollider2D>().enabled = false;
                                }
                            }
                            else {
                                if (PlayerControl.LocalPlayer == KingOfTheHill.usurperPlayer) {
                                    KingOfTheHill.usurperPlayer.transform.position = new Vector3(-1f, 5.35f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(KingOfTheHill.usurperPlayer);
                                }
                                
                                foreach (PlayerControl player in KingOfTheHill.greenTeam) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(-7f, -8.25f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                foreach (PlayerControl player in KingOfTheHill.yellowTeam) {
                                    if (player == PlayerControl.LocalPlayer)
                                        player.transform.position = new Vector3(6.25f, -3.5f, PlayerControl.LocalPlayer.transform.position.z);
                                    Helpers.clearAllTasks(player);
                                }
                                if (PlayerControl.LocalPlayer != null && !createdkingofthehill) {
                                    GameObject greenteamfloor = GameObject.Instantiate(CustomMain.customAssets.greenfloor, PlayerControl.LocalPlayer.transform.parent);
                                    greenteamfloor.name = "greenteamfloor";
                                    greenteamfloor.transform.position = new Vector3(-7f, -8.5f, 0.5f);
                                    GameObject yellowteamfloor = GameObject.Instantiate(CustomMain.customAssets.yellowfloor, PlayerControl.LocalPlayer.transform.parent);
                                    yellowteamfloor.name = "yellowteamfloor";
                                    yellowteamfloor.transform.position = new Vector3(6.25f, -3.75f, 0.5f); 
                                    GameObject greenkingaura = GameObject.Instantiate(CustomMain.customAssets.greenaura, KingOfTheHill.greenKingplayer.transform);
                                    greenkingaura.name = "greenkingaura";
                                    greenkingaura.transform.position = new Vector3(KingOfTheHill.greenKingplayer.transform.position.x, KingOfTheHill.greenKingplayer.transform.position.y, 0.4f);
                                    KingOfTheHill.greenkingaura = greenkingaura;
                                    GameObject yellowkingaura = GameObject.Instantiate(CustomMain.customAssets.yellowaura, KingOfTheHill.yellowKingplayer.transform);
                                    yellowkingaura.name = "yellowkingaura";
                                    yellowkingaura.transform.position = new Vector3(KingOfTheHill.yellowKingplayer.transform.position.x, KingOfTheHill.yellowKingplayer.transform.position.y, 0.4f);
                                    KingOfTheHill.yellowkingaura = yellowkingaura;
                                    GameObject flagzoneone = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                    flagzoneone.name = "flagzoneone";
                                    flagzoneone.transform.position = new Vector3(-9.1f, -2.25f, 0.4f);
                                    KingOfTheHill.flagzoneone = flagzoneone;
                                    GameObject zoneone = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                    zoneone.name = "zoneone";
                                    zoneone.transform.position = new Vector3(-9.1f, -2.25f, 0.5f);
                                    KingOfTheHill.zoneone = zoneone;
                                    GameObject flagzonetwo = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                    flagzonetwo.name = "flagzonetwo";
                                    flagzonetwo.transform.position = new Vector3(4.5f, -7.5f, 0.4f);
                                    KingOfTheHill.flagzonetwo = flagzonetwo;
                                    GameObject zonetwo = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                    zonetwo.name = "zonetwo";
                                    zonetwo.transform.position = new Vector3(4.5f, -7.5f, 0.5f);
                                    KingOfTheHill.zonetwo = zonetwo;
                                    GameObject flagzonethree = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                    flagzonethree.name = "flagzonethree";
                                    flagzonethree.transform.position = new Vector3(3.25f, -15.5f, 0.4f);
                                    KingOfTheHill.flagzonethree = flagzonethree;
                                    GameObject zonethree = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                    zonethree.name = "zonethree";
                                    zonethree.transform.position = new Vector3(3.25f, -15.5f, 0.5f);
                                    KingOfTheHill.zonethree = zonethree;
                                    KingOfTheHill.kingZones.Add(zoneone);
                                    KingOfTheHill.kingZones.Add(zonetwo);
                                    KingOfTheHill.kingZones.Add(zonethree);
                                    createdkingofthehill = true;

                                    // Remove camera use and admin table on Skeld
                                    GameObject cameraStand = GameObject.Find("SurvConsole");
                                    cameraStand.GetComponent<PolygonCollider2D>().enabled = false;
                                    GameObject admin = GameObject.Find("MapRoomConsole");
                                    admin.GetComponent<CircleCollider2D>().enabled = false;
                                }
                            }
                            break;
                        // MiraHQ
                        case 1:
                            if (PlayerControl.LocalPlayer == KingOfTheHill.usurperPlayer) {
                                KingOfTheHill.usurperPlayer.transform.position = new Vector3(2.5f, 11f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(KingOfTheHill.usurperPlayer);
                            }
                            
                            foreach (PlayerControl player in KingOfTheHill.greenTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(-4.45f, 1.75f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in KingOfTheHill.yellowTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(19.5f, 4.7f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdkingofthehill) {
                                GameObject greenteamfloor = GameObject.Instantiate(CustomMain.customAssets.greenfloor, PlayerControl.LocalPlayer.transform.parent);
                                greenteamfloor.name = "greenteamfloor";
                                greenteamfloor.transform.position = new Vector3(-4.45f, 1.5f, 0.5f);
                                GameObject yellowteamfloor = GameObject.Instantiate(CustomMain.customAssets.yellowfloor, PlayerControl.LocalPlayer.transform.parent);
                                yellowteamfloor.name = "yellowteamfloor";
                                yellowteamfloor.transform.position = new Vector3(19.5f, 4.45f, 0.5f); 
                                GameObject greenkingaura = GameObject.Instantiate(CustomMain.customAssets.greenaura, KingOfTheHill.greenKingplayer.transform);
                                greenkingaura.name = "greenkingaura";
                                greenkingaura.transform.position = new Vector3(KingOfTheHill.greenKingplayer.transform.position.x, KingOfTheHill.greenKingplayer.transform.position.y, 0.4f);
                                KingOfTheHill.greenkingaura = greenkingaura;
                                GameObject yellowkingaura = GameObject.Instantiate(CustomMain.customAssets.yellowaura, KingOfTheHill.yellowKingplayer.transform);
                                yellowkingaura.name = "yellowkingaura";
                                yellowkingaura.transform.position = new Vector3(KingOfTheHill.yellowKingplayer.transform.position.x, KingOfTheHill.yellowKingplayer.transform.position.y, 0.4f);
                                KingOfTheHill.yellowkingaura = yellowkingaura;
                                GameObject flagzoneone = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzoneone.name = "flagzoneone";
                                flagzoneone.transform.position = new Vector3(15.25f, 4f, 0.4f);
                                KingOfTheHill.flagzoneone = flagzoneone;
                                GameObject zoneone = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zoneone.name = "zoneone";
                                zoneone.transform.position = new Vector3(15.25f, 4f, 0.5f);
                                KingOfTheHill.zoneone = zoneone;
                                GameObject flagzonetwo = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzonetwo.name = "flagzonetwo";
                                flagzonetwo.transform.position = new Vector3(17.85f, 19.5f, 0.4f);
                                KingOfTheHill.flagzonetwo = flagzonetwo;
                                GameObject zonetwo = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zonetwo.name = "zonetwo";
                                zonetwo.transform.position = new Vector3(17.85f, 19.5f, 0.5f);
                                KingOfTheHill.zonetwo = zonetwo;
                                GameObject flagzonethree = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzonethree.name = "flagzonethree";
                                flagzonethree.transform.position = new Vector3(6.15f, 12.5f, 0.4f);
                                KingOfTheHill.flagzonethree = flagzonethree;
                                GameObject zonethree = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zonethree.name = "zonethree";
                                zonethree.transform.position = new Vector3(6.15f, 12.5f, 0.5f);
                                KingOfTheHill.zonethree = zonethree;
                                KingOfTheHill.kingZones.Add(zoneone);
                                KingOfTheHill.kingZones.Add(zonetwo);
                                KingOfTheHill.kingZones.Add(zonethree);
                                createdkingofthehill = true;

                                // Remove Doorlog use, Decontamintion doors and admin table on MiraHQ
                                GameObject DoorLog = GameObject.Find("SurvLogConsole");
                                DoorLog.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject deconUpperDoor = GameObject.Find("UpperDoor");
                                deconUpperDoor.SetActive(false);
                                GameObject deconLowerDoor = GameObject.Find("LowerDoor");
                                deconLowerDoor.SetActive(false);
                                GameObject deconUpperDoorPanelTop = GameObject.Find("DeconDoorPanel-Top");
                                deconUpperDoorPanelTop.SetActive(false);
                                GameObject deconUpperDoorPanelHigh = GameObject.Find("DeconDoorPanel-High");
                                deconUpperDoorPanelHigh.SetActive(false);
                                GameObject deconUpperDoorPanelBottom = GameObject.Find("DeconDoorPanel-Bottom");
                                deconUpperDoorPanelBottom.SetActive(false);
                                GameObject deconUpperDoorPanelLow = GameObject.Find("DeconDoorPanel-Low");
                                deconUpperDoorPanelLow.SetActive(false);
                                GameObject admin = GameObject.Find("AdminMapConsole");
                                admin.GetComponent<CircleCollider2D>().enabled = false;
                            }
                            break;
                        // Polus
                        case 2:
                            if (PlayerControl.LocalPlayer == KingOfTheHill.usurperPlayer) {
                                KingOfTheHill.usurperPlayer.transform.position = new Vector3(20.5f, -12f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(KingOfTheHill.usurperPlayer);
                            }
                            
                            foreach (PlayerControl player in KingOfTheHill.greenTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(2.25f, -23.75f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in KingOfTheHill.yellowTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(36.35f, -6.15f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdkingofthehill) {
                                GameObject greenteamfloor = GameObject.Instantiate(CustomMain.customAssets.greenfloor, PlayerControl.LocalPlayer.transform.parent);
                                greenteamfloor.name = "greenteamfloor";
                                greenteamfloor.transform.position = new Vector3(2.25f, -24f, 0.5f);
                                GameObject yellowteamfloor = GameObject.Instantiate(CustomMain.customAssets.yellowfloor, PlayerControl.LocalPlayer.transform.parent);
                                yellowteamfloor.name = "yellowteamfloor";
                                yellowteamfloor.transform.position = new Vector3(36.35f, -6.4f, 0.5f); 
                                GameObject greenkingaura = GameObject.Instantiate(CustomMain.customAssets.greenaura, KingOfTheHill.greenKingplayer.transform);
                                greenkingaura.name = "greenkingaura";
                                greenkingaura.transform.position = new Vector3(KingOfTheHill.greenKingplayer.transform.position.x, KingOfTheHill.greenKingplayer.transform.position.y, 0.4f);
                                KingOfTheHill.greenkingaura = greenkingaura;
                                GameObject yellowkingaura = GameObject.Instantiate(CustomMain.customAssets.yellowaura, KingOfTheHill.yellowKingplayer.transform);
                                yellowkingaura.name = "yellowkingaura";
                                yellowkingaura.transform.position = new Vector3(KingOfTheHill.yellowKingplayer.transform.position.x, KingOfTheHill.yellowKingplayer.transform.position.y, 0.4f);
                                KingOfTheHill.yellowkingaura = yellowkingaura;
                                GameObject flagzoneone = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzoneone.name = "flagzoneone";
                                flagzoneone.transform.position = new Vector3(15f, -13.5f, 0.4f);
                                KingOfTheHill.flagzoneone = flagzoneone;
                                GameObject zoneone = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zoneone.name = "zoneone";
                                zoneone.transform.position = new Vector3(15f, -13.5f, 0.5f);
                                KingOfTheHill.zoneone = zoneone;
                                GameObject flagzonetwo = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzonetwo.name = "flagzonetwo";
                                flagzonetwo.transform.position = new Vector3(20.75f, -22.75f, 0.4f);
                                KingOfTheHill.flagzonetwo = flagzonetwo;
                                GameObject zonetwo = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zonetwo.name = "zonetwo";
                                zonetwo.transform.position = new Vector3(20.75f, -22.75f, 0.5f);
                                KingOfTheHill.zonetwo = zonetwo;
                                GameObject flagzonethree = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzonethree.name = "flagzonethree";
                                flagzonethree.transform.position = new Vector3(16.65f, -1.5f, 0.4f);
                                KingOfTheHill.flagzonethree = flagzonethree;
                                GameObject zonethree = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zonethree.name = "zonethree";
                                zonethree.transform.position = new Vector3(16.65f, -1.5f, 0.5f);
                                KingOfTheHill.zonethree = zonethree;
                                KingOfTheHill.kingZones.Add(zoneone);
                                KingOfTheHill.kingZones.Add(zonetwo);
                                KingOfTheHill.kingZones.Add(zonethree);
                                createdkingofthehill = true;

                                // Remove Decon doors, camera use, vitals, admin tables on Polus
                                GameObject lowerdecon = GameObject.Find("LowerDecon");
                                lowerdecon.SetActive(false);
                                GameObject upperdecon = GameObject.Find("UpperDecon");
                                upperdecon.SetActive(false);
                                GameObject survCameras = GameObject.Find("Surv_Panel");
                                survCameras.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject vitals = GameObject.Find("panel_vitals");
                                vitals.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject adminone = GameObject.Find("panel_map");
                                adminone.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject admintwo = GameObject.Find("panel_map (1)");
                                admintwo.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject ramp = GameObject.Find("ramp");
                                ramp.transform.position = new Vector3(ramp.transform.position.x, ramp.transform.position.y, 0.75f);
                                GameObject bathroomVent = GameObject.Find("BathroomVent");
                                bathroomVent.transform.position = new Vector3(34, -10.3f, bathroomVent.transform.position.z);
                            }
                            break;
                        // Dlesk
                        case 3:
                            if (PlayerControl.LocalPlayer == KingOfTheHill.usurperPlayer) {
                                KingOfTheHill.usurperPlayer.transform.position = new Vector3(1f, 5.35f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(KingOfTheHill.usurperPlayer);
                            }
                            
                            foreach (PlayerControl player in KingOfTheHill.greenTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(7f, -8.25f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in KingOfTheHill.yellowTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(-6.25f, -3.5f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdkingofthehill) {
                                GameObject greenteamfloor = GameObject.Instantiate(CustomMain.customAssets.greenfloor, PlayerControl.LocalPlayer.transform.parent);
                                greenteamfloor.name = "greenteamfloor";
                                greenteamfloor.transform.position = new Vector3(7f, -8.5f, 0.5f);
                                GameObject yellowteamfloor = GameObject.Instantiate(CustomMain.customAssets.yellowfloor, PlayerControl.LocalPlayer.transform.parent);
                                yellowteamfloor.name = "yellowteamfloor";
                                yellowteamfloor.transform.position = new Vector3(-6.25f, -3.75f, 0.5f); 
                                GameObject greenkingaura = GameObject.Instantiate(CustomMain.customAssets.greenaura, KingOfTheHill.greenKingplayer.transform);
                                greenkingaura.name = "greenkingaura";
                                greenkingaura.transform.position = new Vector3(KingOfTheHill.greenKingplayer.transform.position.x, KingOfTheHill.greenKingplayer.transform.position.y, 0.4f);
                                KingOfTheHill.greenkingaura = greenkingaura;
                                GameObject yellowkingaura = GameObject.Instantiate(CustomMain.customAssets.yellowaura, KingOfTheHill.yellowKingplayer.transform);
                                yellowkingaura.name = "yellowkingaura";
                                yellowkingaura.transform.position = new Vector3(KingOfTheHill.yellowKingplayer.transform.position.x, KingOfTheHill.yellowKingplayer.transform.position.y, 0.4f);
                                KingOfTheHill.yellowkingaura = yellowkingaura;
                                GameObject flagzoneone = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzoneone.name = "flagzoneone";
                                flagzoneone.transform.position = new Vector3(9.1f, -2.25f, 0.4f);
                                KingOfTheHill.flagzoneone = flagzoneone;
                                GameObject zoneone = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zoneone.name = "zoneone";
                                zoneone.transform.position = new Vector3(9.1f, -2.25f, 0.5f);
                                KingOfTheHill.zoneone = zoneone;
                                GameObject flagzonetwo = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzonetwo.name = "flagzonetwo";
                                flagzonetwo.transform.position = new Vector3(-4.5f, -7.5f, 0.4f);
                                KingOfTheHill.flagzonetwo = flagzonetwo;
                                GameObject zonetwo = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zonetwo.name = "zonetwo";
                                zonetwo.transform.position = new Vector3(-4.5f, -7.5f, 0.5f);
                                KingOfTheHill.zonetwo = zonetwo;
                                GameObject flagzonethree = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzonethree.name = "flagzonethree";
                                flagzonethree.transform.position = new Vector3(-3.25f, -15.5f, 0.4f);
                                KingOfTheHill.flagzonethree = flagzonethree;
                                GameObject zonethree = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zonethree.name = "zonethree";
                                zonethree.transform.position = new Vector3(-3.25f, -15.5f, 0.5f);
                                KingOfTheHill.zonethree = zonethree;
                                KingOfTheHill.kingZones.Add(zoneone);
                                KingOfTheHill.kingZones.Add(zonetwo);
                                KingOfTheHill.kingZones.Add(zonethree);
                                createdkingofthehill = true;

                                // Remove camera use and admin table on Dleks
                                GameObject cameraStand = GameObject.Find("SurvConsole");
                                cameraStand.GetComponent<PolygonCollider2D>().enabled = false;
                                GameObject admin = GameObject.Find("MapRoomConsole");
                                admin.GetComponent<CircleCollider2D>().enabled = false;
                            }
                            break;
                        // Airship
                        case 4:
                            if (PlayerControl.LocalPlayer == KingOfTheHill.usurperPlayer) {
                                KingOfTheHill.usurperPlayer.transform.position = new Vector3(12.25f, 2f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(KingOfTheHill.usurperPlayer);
                            }
                            
                            foreach (PlayerControl player in KingOfTheHill.greenTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(-13.9f, -14.45f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            foreach (PlayerControl player in KingOfTheHill.yellowTeam) {
                                if (player == PlayerControl.LocalPlayer)
                                    player.transform.position = new Vector3(37.35f, -3.25f, PlayerControl.LocalPlayer.transform.position.z);
                                Helpers.clearAllTasks(player);
                            }
                            if (PlayerControl.LocalPlayer != null && !createdkingofthehill) {
                                GameObject greenteamfloor = GameObject.Instantiate(CustomMain.customAssets.greenfloor, PlayerControl.LocalPlayer.transform.parent);
                                greenteamfloor.name = "greenteamfloor";
                                greenteamfloor.transform.position = new Vector3(-13.9f, -14.7f, 0.5f);
                                GameObject yellowteamfloor = GameObject.Instantiate(CustomMain.customAssets.yellowfloor, PlayerControl.LocalPlayer.transform.parent);
                                yellowteamfloor.name = "yellowteamfloor";
                                yellowteamfloor.transform.position = new Vector3(37.35f, -3.5f, 0.5f); 
                                GameObject greenkingaura = GameObject.Instantiate(CustomMain.customAssets.greenaura, KingOfTheHill.greenKingplayer.transform);
                                greenkingaura.name = "greenkingaura";
                                greenkingaura.transform.position = new Vector3(KingOfTheHill.greenKingplayer.transform.position.x, KingOfTheHill.greenKingplayer.transform.position.y, 0.4f);
                                KingOfTheHill.greenkingaura = greenkingaura;
                                GameObject yellowkingaura = GameObject.Instantiate(CustomMain.customAssets.yellowaura, KingOfTheHill.yellowKingplayer.transform);
                                yellowkingaura.name = "yellowkingaura";
                                yellowkingaura.transform.position = new Vector3(KingOfTheHill.yellowKingplayer.transform.position.x, KingOfTheHill.yellowKingplayer.transform.position.y, 0.4f);
                                KingOfTheHill.yellowkingaura = yellowkingaura;
                                GameObject flagzoneone = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzoneone.name = "flagzoneone";
                                flagzoneone.transform.position = new Vector3(-8.75f, 5.1f, 0.4f);
                                KingOfTheHill.flagzoneone = flagzoneone;
                                GameObject zoneone = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zoneone.name = "zoneone";
                                zoneone.transform.position = new Vector3(-8.75f, 5.1f, 0.5f);
                                KingOfTheHill.zoneone = zoneone;
                                GameObject flagzonetwo = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzonetwo.name = "flagzonetwo";
                                flagzonetwo.transform.position = new Vector3(19.9f, 11.25f, 0.4f);
                                KingOfTheHill.flagzonetwo = flagzonetwo;
                                GameObject zonetwo = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zonetwo.name = "zonetwo";
                                zonetwo.transform.position = new Vector3(19.9f, 11.25f, 0.5f);
                                KingOfTheHill.zonetwo = zonetwo;
                                GameObject flagzonethree = GameObject.Instantiate(CustomMain.customAssets.whiteflag, PlayerControl.LocalPlayer.transform.parent);
                                flagzonethree.name = "flagzonethree";
                                flagzonethree.transform.position = new Vector3(16.3f, -8.6f, 0.4f);
                                KingOfTheHill.flagzonethree = flagzonethree;
                                GameObject zonethree = GameObject.Instantiate(CustomMain.customAssets.whitebase, PlayerControl.LocalPlayer.transform.parent);
                                zonethree.name = "zonethree";
                                zonethree.transform.position = new Vector3(16.3f, -8.6f, 0.5f);
                                KingOfTheHill.zonethree = zonethree;
                                KingOfTheHill.kingZones.Add(zoneone);
                                KingOfTheHill.kingZones.Add(zonetwo);
                                KingOfTheHill.kingZones.Add(zonethree);
                                createdkingofthehill = true;

                                // Remove camera use, admin table, vitals, electrical doors on Airship
                                GameObject cameras = GameObject.Find("task_cams");
                                cameras.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject admin = GameObject.Find("panel_cockpit_map");
                                admin.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject vitals = GameObject.Find("panel_vitals");
                                vitals.GetComponent<CircleCollider2D>().enabled = false;
                                GameObject LeftDoorTop = GameObject.Find("LeftDoorTop");
                                LeftDoorTop.SetActive(false);
                                GameObject TopLeftVert = GameObject.Find("TopLeftVert");
                                TopLeftVert.SetActive(false);
                                GameObject TopLeftHort = GameObject.Find("TopLeftHort");
                                TopLeftHort.SetActive(false);
                                GameObject BottomHort = GameObject.Find("BottomHort");
                                BottomHort.SetActive(false);
                                GameObject TopCenterHort = GameObject.Find("TopCenterHort");
                                TopCenterHort.SetActive(false);
                                GameObject LeftVert = GameObject.Find("LeftVert");
                                LeftVert.SetActive(false);
                                GameObject RightVert = GameObject.Find("RightVert");
                                RightVert.SetActive(false);
                                GameObject TopRightVert = GameObject.Find("TopRightVert");
                                TopRightVert.SetActive(false);
                                GameObject TopRightHort = GameObject.Find("TopRightHort");
                                TopRightHort.SetActive(false);
                                GameObject BottomRightHort = GameObject.Find("BottomRightHort");
                                BottomRightHort.SetActive(false);
                                GameObject BottomRightVert = GameObject.Find("BottomRightVert");
                                BottomRightVert.SetActive(false);
                                GameObject LeftDoorBottom = GameObject.Find("LeftDoorBottom");
                                LeftDoorBottom.SetActive(false);
                            }
                            break;
                    }
                    new CustomMessage("Time Left: ", KingOfTheHill.matchDuration, -1, -1.3f, 10);
                    new CustomMessage(KingOfTheHill.kingpointCounter, KingOfTheHill.matchDuration, -1, 1.9f, 12);

                    // Add Arrows pointing the zones
                    if (KingOfTheHill.localArrows.Count == 0 && KingOfTheHill.localArrows.Count < 3) {
                        KingOfTheHill.localArrows.Add(new Arrow(KingOfTheHill.zoneonecolor));
                        KingOfTheHill.localArrows.Add(new Arrow(KingOfTheHill.zonetwocolor));
                        KingOfTheHill.localArrows.Add(new Arrow(KingOfTheHill.zonethreecolor));
                    }
                    KingOfTheHill.localArrows[0].arrow.SetActive(true);
                    KingOfTheHill.localArrows[1].arrow.SetActive(true);
                    KingOfTheHill.localArrows[2].arrow.SetActive(true);
                }
            }
        }

        [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.BeginCrewmate))]
        class BeginCrewmatePatch
        {
            public static void Prefix(IntroCutscene __instance, ref Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam) {
                setupIntroTeamIcons(__instance, ref yourTeam);
            }

            public static void Postfix(IntroCutscene __instance, ref Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam) {
                setupIntroTeam(__instance, ref yourTeam);
            }
        }

        [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.BeginImpostor))]
        class BeginImpostorPatch
        {
            public static void Prefix(IntroCutscene __instance, ref Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam) {
                setupIntroTeamIcons(__instance, ref yourTeam);
            }

            public static void Postfix(IntroCutscene __instance, ref Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam) {
                setupIntroTeam(__instance, ref yourTeam);
            }
        }

        public static void clearSwipeCardTask() {

            bool removeSwipeCard = CustomOptionHolder.removeSwipeCard.getBool();

            if (removeSwipeCard && removedSwipe == false && PlayerControl.GameOptions.MapId != 1 && PlayerControl.GameOptions.MapId != 4) {
                foreach (PlayerControl myplayer in PlayerControl.AllPlayerControls) {
                    if (myplayer != Joker.joker && myplayer != RoleThief.rolethief && myplayer != Pyromaniac.pyromaniac && myplayer != TreasureHunter.treasureHunter && myplayer != Devourer.devourer && myplayer != Renegade.renegade && myplayer != Minion.minion && myplayer != BountyHunter.bountyhunter && myplayer != Trapper.trapper && myplayer != Yinyanger.yinyanger && myplayer != Challenger.challenger && !myplayer.Data.Role.IsImpostor) {
                        var toRemove = new List<PlayerTask>();
                        foreach (PlayerTask task in myplayer.myTasks)
                            if (task.TaskType == TaskTypes.SwipeCard)
                                toRemove.Add(task);
                        foreach (PlayerTask task in toRemove)
                            if (task.TaskType == TaskTypes.SwipeCard)
                                myplayer.RemoveTask(task);
                    }
                }
                removedSwipe = true;
            }
        }

        public static void removeAirshipDoors() {

            bool removeAirshipDoors = CustomOptionHolder.removeAirshipDoors.getBool();

            List<GameObject> doors = new List<GameObject>();

            if (removeAirshipDoors && removedAirshipDoors == false && PlayerControl.GameOptions.MapId == 4) {
                GameObject celldoor01 = GameObject.Find("doorsideOpen (2)");
                doors.Add(celldoor01);
                GameObject celldoor02 = GameObject.Find("door_vault");
                doors.Add(celldoor02);
                GameObject celldoor04 = GameObject.Find("door_gap");
                doors.Add(celldoor04);

                GameObject bighallwaydoor01 = GameObject.Find("Door_VertOpen");
                doors.Add(bighallwaydoor01);
                GameObject bighallwaydoor02 = GameObject.Find("Door_VertOpen (4)");
                doors.Add(bighallwaydoor02);
                GameObject bighallwaydoor03 = GameObject.Find("Door_HortOpen");
                doors.Add(bighallwaydoor03);
                GameObject bighallwaydoor04 = GameObject.Find("Door_HortOpen (1)");
                doors.Add(bighallwaydoor04);

                GameObject kitchendoor01 = GameObject.Find("Door_VertOpen (1)");
                doors.Add(kitchendoor01);
                GameObject kitchendoor02 = GameObject.Find("Door_VertOpen (2)");
                doors.Add(kitchendoor02);
                GameObject kitchendoor03 = GameObject.Find("Door_VertOpen (3)");
                doors.Add(kitchendoor03);

                GameObject medbeydoor01 = GameObject.Find("Door_VertOpen (10)");
                doors.Add(medbeydoor01);
                GameObject medbeydoor02 = GameObject.Find("Door_HortOpen (3)");
                doors.Add(medbeydoor02);

                GameObject recorddoor01 = GameObject.Find("Door_VertOpen (11)");
                doors.Add(recorddoor01);
                GameObject recorddoor02 = GameObject.Find("Door_VertOpen (12)");
                doors.Add(recorddoor02);
                GameObject recorddoor03 = GameObject.Find("Door_HortOpen (2)");
                doors.Add(recorddoor03);

                GameObject hallway01 = GameObject.Find("Door_VertOpen (5)");
                doors.Add(hallway01);
                GameObject hallway02 = GameObject.Find("Door_VertOpen (6)");
                doors.Add(hallway02);

                foreach (GameObject door in doors) {
                    door.GetComponent<BoxCollider2D>().enabled = false;
                    door.GetComponent<SpriteRenderer>().enabled = false;
                }

                removedAirshipDoors = true;
            }
        }

        public static void activateSenseiMap() {

            bool activeSensei = CustomOptionHolder.activateSenseiMap.getBool();

            // Activate custom map only on Skeld if the opcion is activated
            if (activeSensei && activatedSensei == false && PlayerControl.GameOptions.MapId == 0) {

                // Spawn map + assign shadow and materials layers
                GameObject senseiMap = GameObject.Instantiate(CustomMain.customAssets.customMap, PlayerControl.LocalPlayer.transform.parent);
                senseiMap.name = "HalconUI";
                senseiMap.transform.position = new Vector3(-1.5f, -1.4f, 15.05f);
                senseiMap.transform.GetChild(0).gameObject.layer = 9; // Ship Layer for HalconColisions
                senseiMap.transform.GetChild(0).transform.GetChild(0).gameObject.layer = 11; // Object Layer for HalconShadows
                senseiMap.transform.GetChild(0).transform.GetChild(1).gameObject.layer = 9; // Ship Layer for HalconAboveItems
                Material shadowShader = null;
                GameObject background = GameObject.Find("SkeldShip(Clone)/AdminHallway");
                {
                    SpriteRenderer sp = background.GetComponent<SpriteRenderer>();
                    if (sp != null) {
                        shadowShader = sp.material;
                    }
                }
                {
                    SpriteRenderer sp = senseiMap.GetComponent<SpriteRenderer>();
                    if (sp != null && shadowShader != null) {
                        sp.material = shadowShader;
                        senseiMap.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().material = shadowShader;
                    }
                }

                // Assign colliders objets, find halconCollisions to be the main parent
                GameObject halconCollisions = senseiMap.transform.GetChild(0).transform.gameObject; 
                
                // Area colliders rebuilded for showing map names
                GameObject colliderAdmin = GameObject.Find("SkeldShip(Clone)/Admin/Room");
                colliderAdmin.transform.SetParent(halconCollisions.transform);
                colliderAdmin.name = "RoomAdmin";
                foreach (Collider2D c in colliderAdmin.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderAdmin.transform.position = new Vector3(0, 0, 0);
                Vector2[] myAdminpoints = { new Vector2(10.09f, -3.65f), new Vector2(1.96f, -3.65f), new Vector2(0.28f, -6.09f), new Vector2(3.97f, -10.45f), new Vector2(7.12f, -10.43f) };
                colliderAdmin.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myAdminpoints;

                GameObject colliderCafeteria = GameObject.Find("SkeldShip(Clone)/Cafeteria/Room");
                colliderCafeteria.transform.SetParent(halconCollisions.transform);
                colliderCafeteria.name = "RoomCafeteria";
                foreach (Collider2D c in colliderCafeteria.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderCafeteria.transform.position = new Vector3(0, 0, 0);
                Vector2[] myCafeteriapoints = { new Vector2(4f, 3.35f), new Vector2(-2f, 3.35f), new Vector2(-2f, 4f), new Vector2(-4.5f, 6f), new Vector2(-4.5f, 0.55f), new Vector2(-2.8f, 0f), new Vector2(-2.8f, -2.64f), new Vector2(4, -2.64f) };
                colliderCafeteria.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myCafeteriapoints;

                GameObject colliderCockpit = GameObject.Find("SkeldShip(Clone)/Cockpit/Room");
                colliderCockpit.transform.SetParent(halconCollisions.transform);
                colliderCockpit.name = "RoomCookpit";
                foreach (Collider2D c in colliderCockpit.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderCockpit.transform.position = new Vector3(0, 0, 0);
                Vector2[] myCockpitpoints = { new Vector2(5f, -10f), new Vector2(5f, -13f), new Vector2(8.5f, -13f), new Vector2(8.5f, -10f) };
                colliderCockpit.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myCockpitpoints;

                GameObject colliderWeapons = GameObject.Find("SkeldShip(Clone)/Weapons/Room");
                colliderWeapons.transform.SetParent(halconCollisions.transform);
                colliderWeapons.name = "RoomWeapons";
                foreach (Collider2D c in colliderWeapons.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderWeapons.transform.position = new Vector3(0, 0, 0);
                Vector2[] myWeaponspoints = { new Vector2(12.5f, 0.5f), new Vector2(8.5f, 1.35f), new Vector2(8.5f, -3.5f), new Vector2(12.5f, -3.5f) };
                colliderWeapons.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myWeaponspoints;

                GameObject colliderLifeSupport = GameObject.Find("SkeldShip(Clone)/LifeSupport/Room");
                colliderLifeSupport.transform.SetParent(halconCollisions.transform);
                colliderLifeSupport.name = "RoomLifeSupport";
                foreach (Collider2D c in colliderLifeSupport.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderLifeSupport.transform.position = new Vector3(0, 0, 0);
                Vector2[] myLifeSupportpoints = { new Vector2(-6.66f, 1.8f), new Vector2(-8.56f, 0.75f), new Vector2(-9.1f, 0.5f), new Vector2(-9.1f, -0.6f), new Vector2(-6.3f, -0.6f), new Vector2(-6.3f, 1.8f) };
                colliderLifeSupport.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myLifeSupportpoints;

                GameObject colliderShields = GameObject.Find("SkeldShip(Clone)/Shields/Room");
                colliderShields.transform.SetParent(halconCollisions.transform);
                colliderShields.name = "RoomShields";
                foreach (Collider2D c in colliderShields.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderShields.transform.position = new Vector3(0, 0, 0);
                Vector2[] myShieldspoints = { new Vector2(4.3f, 0.3f), new Vector2(4.3f, -3.1f), new Vector2(8f, -3.1f), new Vector2(8f, 0.3f) };
                colliderShields.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myShieldspoints;

                GameObject colliderElectrical = GameObject.Find("SkeldShip(Clone)/Electrical/Room");
                colliderElectrical.transform.SetParent(halconCollisions.transform);
                colliderElectrical.name = "RoomElectrical";
                foreach (Collider2D c in colliderElectrical.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderElectrical.transform.position = new Vector3(0, 0, 0);
                Vector2[] myElectricalpoints = { new Vector2(-3.9f, -9.54f), new Vector2(-3.9f, -6.69f), new Vector2(-6.7f, -6.69f), new Vector2(-6.7f, -9.54f), new Vector2(-7.3f, -9.54f), new Vector2(-7.3f, -12.9f), new Vector2(-3.39f, -12.9f), new Vector2(-3.39f, -9.54f) };
                colliderElectrical.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myElectricalpoints;


                GameObject colliderReactor = GameObject.Find("SkeldShip(Clone)/Reactor/Room");
                colliderReactor.transform.SetParent(halconCollisions.transform);
                colliderReactor.name = "RoomReactor";
                foreach (Collider2D c in colliderReactor.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderReactor.transform.position = new Vector3(0, 0, 0);
                Vector2[] myReactorpoints = { new Vector2(-21, 2f), new Vector2(-21.5f, 0f), new Vector2(-21f, -4.2f), new Vector2(-12.6f, -2.79f), new Vector2(-12.85f, -1.25f), new Vector2(-12.6f, -0.1f) };
                colliderReactor.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myReactorpoints;

                GameObject colliderStorage = GameObject.Find("SkeldShip(Clone)/Storage/Room");
                colliderStorage.transform.SetParent(halconCollisions.transform);
                colliderStorage.name = "RoomStorage";
                foreach (Collider2D c in colliderStorage.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderStorage.transform.position = new Vector3(0, 0, 0);
                Vector2[] myStoragepoints = { new Vector2(-11.2f, -5.7f), new Vector2(-17.4f, -9f), new Vector2(-14.91f, -11.23f), new Vector2(-15.19f, -11.61f), new Vector2(-12.46f, -13.07f), new Vector2(-9.13f, -14.07f), new Vector2(-8.78f, -13.24f), new Vector2(-7.38f, -13.24f), new Vector2(-7.4f, -9.52f), new Vector2(-7.2f, -9.52f), new Vector2(-7.2f, -7.2f) };
                colliderStorage.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myStoragepoints;

                GameObject colliderRightEngine = GameObject.Find("SkeldShip(Clone)/RightEngine/Room");
                colliderRightEngine.transform.SetParent(halconCollisions.transform);
                colliderRightEngine.name = "RoomRightEngine";
                foreach (Collider2D c in colliderRightEngine.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderRightEngine.transform.position = new Vector3(0, 0, 0);
                Vector2[] myRightEnginepoints = { new Vector2(-20f, -4.5f), new Vector2(-19.15f, -6.95f), new Vector2(-16.8f, -8.9f), new Vector2(-11f, -5.1f), new Vector2(-11.75f, -4.75f), new Vector2(-12.65f, -3.25f) };
                colliderRightEngine.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myRightEnginepoints;

                GameObject colliderLeftEngine = GameObject.Find("SkeldShip(Clone)/LeftEngine/Room");
                colliderLeftEngine.transform.SetParent(halconCollisions.transform);
                colliderLeftEngine.name = "RoomLeftEngine";
                foreach (Collider2D c in colliderLeftEngine.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderLeftEngine.transform.position = new Vector3(0, 0, 0);
                Vector2[] myLeftEnginepoints = { new Vector2(-16.68f, 7.17f), new Vector2(-18.86f, 4.95f), new Vector2(-20.28f, 2.03f), new Vector2(-12.84f, 0.3f), new Vector2(-11.93f, 1.85f), new Vector2(-10.87f, 2.85f) };
                colliderLeftEngine.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myLeftEnginepoints;

                GameObject colliderComms = GameObject.Find("SkeldShip(Clone)/Comms/Room");
                colliderComms.transform.SetParent(halconCollisions.transform);
                colliderComms.name = "RoomComms";
                foreach (Collider2D c in colliderComms.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderComms.transform.position = new Vector3(0, 0, 0);
                Vector2[] myCommspoints = { new Vector2(4.3f, 4.5f), new Vector2(4.3f, 0.7f), new Vector2(8f, 0.7f), new Vector2(8f, 4.5f) };
                colliderComms.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myCommspoints;

                GameObject colliderSecurity = GameObject.Find("SkeldShip(Clone)/Security/Room");
                colliderSecurity.transform.SetParent(halconCollisions.transform);
                colliderSecurity.name = "RoomSecurity";
                foreach (Collider2D c in colliderSecurity.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderSecurity.transform.position = new Vector3(0, 0, 0);
                Vector2[] mySecuritypoints = { new Vector2(-7.9f, 10.3f), new Vector2(-7.9f, 8.25f), new Vector2(-3.75f, 8.25f), new Vector2(-3.75f, 10.3f) };
                colliderSecurity.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = mySecuritypoints;

                GameObject colliderMedical = GameObject.Find("SkeldShip(Clone)/Medical/Room");
                colliderMedical.transform.SetParent(halconCollisions.transform);
                colliderMedical.name = "RoomMedical";
                foreach (Collider2D c in colliderMedical.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderMedical.transform.position = new Vector3(0, 0, 0);
                Vector2[] myMedicalpoints = { new Vector2(-4.8f, 1.3f), new Vector2(-5.99f, 1.3f), new Vector2(-5.99f, -1.75f), new Vector2(-8.31f, -2.5f), new Vector2(-7.5f, -2.5f), new Vector2(-7.5f, -3.9f), new Vector2(-3.23f, -3.9f), new Vector2(-3.23f, -1.8f), new Vector2(-3.23f, -0.18f), new Vector2(-4.8f, -0.18f) };
                colliderMedical.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myMedicalpoints;

                // HullItems objects
                GameObject halconHullItems = senseiMap.transform.GetChild(1).transform.gameObject; // find halconHullItems to the parent
                GameObject skeldhatch0001 = GameObject.Find("hatch0001");
                skeldhatch0001.transform.SetParent(halconHullItems.transform);
                skeldhatch0001.transform.position = new Vector3(-10.33f, -14.025f, skeldhatch0001.transform.position.z);
                GameObject skeldshieldborder_off = GameObject.Find("shieldborder_off");
                skeldshieldborder_off.transform.SetParent(halconHullItems.transform);
                skeldshieldborder_off.transform.position = new Vector3(10.85f, -6.2f, skeldshieldborder_off.transform.position.z);
                GameObject skeldthruster0001lowestone = GameObject.Find("thruster0001 (1)");
                skeldthruster0001lowestone.transform.SetParent(halconHullItems.transform);
                skeldthruster0001lowestone.transform.position = new Vector3(-24.4f, -9.25f, skeldthruster0001lowestone.transform.position.z);
                GameObject skeldthruster0001lowerone = GameObject.Find("thruster0001 (2)");
                skeldthruster0001lowerone.transform.SetParent(halconHullItems.transform);
                skeldthruster0001lowerone.transform.position = new Vector3(-25.75f, -6, skeldthruster0001lowerone.transform.position.z);
                GameObject skeldthruster0001upperone = GameObject.Find("thruster0001");
                skeldthruster0001upperone.transform.SetParent(halconHullItems.transform);
                skeldthruster0001upperone.transform.position = new Vector3(-25.75f, 3.275f, skeldthruster0001upperone.transform.position.z);
                GameObject skeldthruster0001higherone = GameObject.Find("thruster0001 (3)");
                skeldthruster0001higherone.transform.SetParent(halconHullItems.transform);
                skeldthruster0001higherone.transform.position = new Vector3(-24.4f, 5.9f, skeldthruster0001higherone.transform.position.z);
                GameObject skeldthruster0001middleone = GameObject.Find("thrusterbig0001");
                skeldthruster0001middleone.transform.SetParent(halconHullItems.transform);
                skeldthruster0001middleone.transform.position = new Vector3(-28.15f, -2, skeldthruster0001middleone.transform.position.z);
                GameObject skeldweapongun = GameObject.Find("WeaponGun");
                skeldweapongun.transform.SetParent(halconHullItems.transform);
                skeldweapongun.transform.position = new Vector3(16.5f, -1.865f, skeldweapongun.transform.position.z);
                GameObject skeldlowershield = GameObject.Find("shield_off");
                skeldlowershield.transform.SetParent(halconHullItems.transform);
                skeldlowershield.transform.position = new Vector3(10.9f, -6.65f, skeldlowershield.transform.position.z);
                GameObject skelduppershield = GameObject.Find("shield_off (1)");
                skelduppershield.transform.SetParent(halconHullItems.transform);
                skelduppershield.transform.position = new Vector3(10.8f, -5.85f, skelduppershield.transform.position.z);
                GameObject skeldstarfield = GameObject.Find("starfield");
                skeldstarfield.transform.SetParent(halconHullItems.transform);
                skeldstarfield.transform.position = new Vector3(3, -4.5f, skeldstarfield.transform.position.z);

                // Admin objects
                GameObject halconAdmin = senseiMap.transform.GetChild(2).transform.gameObject; // find halconAdmin to be the parent
                GameObject skeldAdminVent = GameObject.Find("AdminVent");
                skeldAdminVent.transform.SetParent(halconAdmin.transform);
                skeldAdminVent.transform.position = new Vector3(4.17f, -10.5f, skeldAdminVent.transform.position.z);
                GameObject skeldadmintable = GameObject.Find("admin_bridge");
                skeldadmintable.transform.SetParent(halconAdmin.transform);
                skeldadmintable.transform.position = new Vector3(5.01f, -6.675f, skeldadmintable.transform.position.z);
                GameObject skeldSwipeCardConsole = GameObject.Find("SwipeCardConsole");
                skeldSwipeCardConsole.transform.SetParent(halconAdmin.transform);
                skeldSwipeCardConsole.transform.position = new Vector3(6.07f, -6.575f, skeldSwipeCardConsole.transform.position.z);
                GameObject skeldMapRoomConsole = GameObject.Find("MapRoomConsole");
                skeldMapRoomConsole.transform.SetParent(halconAdmin.transform);
                skeldMapRoomConsole.transform.position = new Vector3(3.95f, -6.575f, skeldMapRoomConsole.transform.position.z);
                GameObject skeldLeftScreen = GameObject.Find("LeftScreen");
                skeldLeftScreen.transform.SetParent(halconAdmin.transform);
                skeldLeftScreen.transform.position = new Vector3(3.56f, -3.85f, skeldLeftScreen.transform.position.z);
                GameObject skeldRightScreen = GameObject.Find("RightScreen");
                skeldRightScreen.transform.SetParent(halconAdmin.transform);
                skeldRightScreen.transform.position = new Vector3(5.55f, -3.85f, skeldRightScreen.transform.position.z);
                GameObject skeldAdminUploadDataConsole = GameObject.Find("SkeldShip(Clone)/Admin/Ground/admin_walls/UploadDataConsole");
                skeldAdminUploadDataConsole.transform.SetParent(halconAdmin.transform);
                skeldAdminUploadDataConsole.transform.position = new Vector3(8.975f, -3.86f, skeldAdminUploadDataConsole.transform.position.z);
                GameObject skeldAdminNoOxyConsole = GameObject.Find("SkeldShip(Clone)/Admin/Ground/admin_walls/NoOxyConsole");
                skeldAdminNoOxyConsole.transform.SetParent(halconAdmin.transform);
                skeldAdminNoOxyConsole.transform.position = new Vector3(2.65f, -4f, skeldAdminNoOxyConsole.transform.position.z);
                GameObject skeldAdminFixWiringConsole = GameObject.Find("SkeldShip(Clone)/Admin/Ground/admin_walls/FixWiringConsole");
                skeldAdminFixWiringConsole.transform.SetParent(halconAdmin.transform);
                skeldAdminFixWiringConsole.transform.position = new Vector3(6.47f, -3.87f, skeldAdminFixWiringConsole.transform.position.z);
                GameObject skeldmapComsChairs = GameObject.Find("map_ComsChairs");
                skeldmapComsChairs.transform.SetParent(halconAdmin.transform);
                skeldmapComsChairs.transform.position = new Vector3(4.585f, -4.38f, skeldmapComsChairs.transform.position.z);
                skeldadmintable.transform.GetChild(0).gameObject.SetActive(false); // Deactivate map animation

                // Cafeteria objects
                GameObject halconCafeteria = senseiMap.transform.GetChild(3).transform.gameObject; // find halconCafeteria to be the parent
                GameObject skeldCafeVent = GameObject.Find("CafeVent");
                skeldCafeVent.transform.SetParent(halconCafeteria.transform);
                skeldCafeVent.transform.position = new Vector3(-4.7f, 4, skeldCafeVent.transform.position.z);
                GameObject skeldCafeGarbageConsole = GameObject.Find("SkeldShip(Clone)/Cafeteria/Ground/GarbageConsole");
                skeldCafeGarbageConsole.transform.SetParent(halconCafeteria.transform);
                skeldCafeGarbageConsole.transform.position = new Vector3(4.69f, 4, skeldCafeGarbageConsole.transform.position.z);
                GameObject skeldCafeFixWiringConsole = GameObject.Find("SkeldShip(Clone)/Cafeteria/Ground/FixWiringConsole");
                skeldCafeFixWiringConsole.transform.SetParent(halconCafeteria.transform);
                skeldCafeFixWiringConsole.transform.position = new Vector3(-4.15f, 2.62f, skeldCafeFixWiringConsole.transform.position.z);
                GameObject skeldCafeDataConsole = GameObject.Find("SkeldShip(Clone)/Cafeteria/Ground/DataConsole");
                skeldCafeDataConsole.transform.SetParent(halconCafeteria.transform);
                skeldCafeDataConsole.transform.position = new Vector3(-3.75f, 6.05f, skeldCafeDataConsole.transform.position.z);
                GameObject skeldCafeEmergencyConsole = GameObject.Find("EmergencyConsole");
                skeldCafeEmergencyConsole.transform.SetParent(halconCafeteria.transform);
                skeldCafeEmergencyConsole.transform.position = new Vector3(-0.65f, 1, skeldCafeEmergencyConsole.transform.position.z);

                // nav objects
                GameObject halconCockpit = senseiMap.transform.GetChild(4).transform.gameObject; // find halconCockpit to be the parent
                GameObject skeldNavVentNorth = GameObject.Find("NavVentNorth");
                skeldNavVentNorth.transform.SetParent(halconCockpit.transform);
                skeldNavVentNorth.transform.position = new Vector3(6.5f, -13.15f, skeldNavVentNorth.transform.position.z);
                GameObject skeldNavVentSouth = GameObject.Find("NavVentSouth");
                skeldNavVentSouth.transform.SetParent(halconCockpit.transform);
                skeldNavVentSouth.transform.position = new Vector3(6.5f, -15.05f, skeldNavVentSouth.transform.position.z);
                GameObject skeldNavDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Cockpit/DivertPowerConsole");
                skeldNavDivertPowerConsole.transform.SetParent(halconCockpit.transform);
                skeldNavDivertPowerConsole.transform.position = new Vector3(6.07f, -12.55f, skeldNavDivertPowerConsole.transform.position.z);
                GameObject skeldNavStabilizeSteeringConsole = GameObject.Find("StabilizeSteeringConsole");
                skeldNavStabilizeSteeringConsole.transform.SetParent(halconCockpit.transform);
                skeldNavStabilizeSteeringConsole.transform.position = new Vector3(9.21f, -14.17f, skeldNavStabilizeSteeringConsole.transform.position.z);
                GameObject skeldNavChartCourseConsole = GameObject.Find("ChartCourseConsole");
                skeldNavChartCourseConsole.transform.SetParent(halconCockpit.transform);
                skeldNavChartCourseConsole.transform.position = new Vector3(8.01f, -13.1f, skeldNavChartCourseConsole.transform.position.z);
                GameObject skeldNavUploadDataConsole = GameObject.Find("SkeldShip(Clone)/Cockpit/Ground/UploadDataConsole");
                skeldNavUploadDataConsole.transform.SetParent(halconCockpit.transform);
                skeldNavUploadDataConsole.transform.position = new Vector3(6.59f, -12.55f, skeldNavUploadDataConsole.transform.position.z);
                GameObject skeldNavnav_chairmid = GameObject.Find("nav_chairmid");
                skeldNavnav_chairmid.transform.SetParent(halconCockpit.transform);
                skeldNavnav_chairmid.transform.position = new Vector3(8.5f, -14.1f, skeldNavnav_chairmid.transform.position.z);
                GameObject skeldNavnav_chairback = GameObject.Find("nav_chairback");
                skeldNavnav_chairback.transform.SetParent(halconCockpit.transform);
                skeldNavnav_chairback.transform.position = new Vector3(7.7f, -13.4f, skeldNavnav_chairback.transform.position.z);

                // Weapons objects
                GameObject halconWeapons = senseiMap.transform.GetChild(5).transform.gameObject; // find halconWeapons to be the parent
                GameObject skeldWeaponsVent = GameObject.Find("WeaponsVent");
                skeldWeaponsVent.transform.SetParent(halconWeapons.transform);
                skeldWeaponsVent.transform.position = new Vector3(12.25f, -2.85f, skeldWeaponsVent.transform.position.z);
                GameObject skeldWeaponsUploadDataConsole = GameObject.Find("SkeldShip(Clone)/Weapons/Ground/UploadDataConsole");
                skeldWeaponsUploadDataConsole.transform.SetParent(halconWeapons.transform);
                skeldWeaponsUploadDataConsole.transform.position = new Vector3(11.33f, 0.3f, skeldWeaponsUploadDataConsole.transform.position.z);
                GameObject skeldWeaponsDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Weapons/Ground/weap_wall/DivertPowerConsole");
                skeldWeaponsDivertPowerConsole.transform.SetParent(halconWeapons.transform);
                skeldWeaponsDivertPowerConsole.transform.position = new Vector3(14.24f, 0.075f, skeldWeaponsDivertPowerConsole.transform.position.z);
                GameObject skeldWeaponsHeadAnim = GameObject.Find("bullettop-capglo0001");
                skeldWeaponsHeadAnim.transform.SetParent(halconWeapons.transform);
                skeldWeaponsHeadAnim.transform.position = new Vector3(10.14f, 0.525f, skeldWeaponsHeadAnim.transform.position.z);
                GameObject skeldWeaponsConsole = GameObject.Find("WeaponConsole");
                skeldWeaponsConsole.transform.SetParent(halconWeapons.transform);
                skeldWeaponsConsole.transform.position = new Vector3(11.84f, -1.25f, skeldWeaponsConsole.transform.position.z);

                // LifeSupport objects
                GameObject halconLifeSupport = senseiMap.transform.GetChild(6).transform.gameObject; // find halconLifeSupport to be the parent
                GameObject skeldLifeSupportGarbageConsole = GameObject.Find("SkeldShip(Clone)/LifeSupport/Ground/GarbageConsole");
                skeldLifeSupportGarbageConsole.transform.SetParent(halconLifeSupport.transform);
                skeldLifeSupportGarbageConsole.transform.position = new Vector3(-10.665f, 0.37f, skeldLifeSupportGarbageConsole.transform.position.z);
                GameObject skeldLifeSupportDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/LifeSupport/Ground/DivertPowerConsole");
                skeldLifeSupportDivertPowerConsole.transform.SetParent(halconLifeSupport.transform);
                skeldLifeSupportDivertPowerConsole.transform.position = new Vector3(-7.808f, 2.07f, skeldLifeSupportDivertPowerConsole.transform.position.z);
                GameObject skeldLifeSupportCleanFilterConsole = GameObject.Find("SkeldShip(Clone)/LifeSupport/Ground/CleanFilterConsole");
                skeldLifeSupportCleanFilterConsole.transform.SetParent(halconLifeSupport.transform);
                skeldLifeSupportCleanFilterConsole.transform.position = new Vector3(-9.8f, 0.82f, skeldLifeSupportCleanFilterConsole.transform.position.z);
                GameObject skeldLifeSupportLifeSuppTank = GameObject.Find("SkeldShip(Clone)/LifeSupport/Ground/LifeSuppTank");
                skeldLifeSupportLifeSuppTank.transform.SetParent(halconLifeSupport.transform);
                skeldLifeSupportLifeSuppTank.transform.position = new Vector3(-8.45f, 0.6f, skeldLifeSupportLifeSuppTank.transform.position.z);
                GameObject skeldBigYVent = GameObject.Find("BigYVent");
                skeldBigYVent.transform.SetParent(halconLifeSupport.transform);
                skeldBigYVent.transform.position = new Vector3(-9.65f, -0.4f, skeldBigYVent.transform.position.z);

                // Shields objects
                GameObject halconShields = senseiMap.transform.GetChild(7).transform.gameObject; // find halconShields to be the parent
                GameObject skeldShieldsVent = GameObject.Find("ShieldsVent");
                skeldShieldsVent.transform.SetParent(halconShields.transform);
                skeldShieldsVent.transform.position = new Vector3(5.575f, -1f, skeldShieldsVent.transform.position.z);
                GameObject skeldShieldsDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Shields/Ground/shields_floor/shields_wallside/DivertPowerConsole");
                skeldShieldsDivertPowerConsole.transform.SetParent(halconShields.transform);
                skeldShieldsDivertPowerConsole.transform.position = new Vector3(8.962f, 0.7f, skeldShieldsDivertPowerConsole.transform.position.z);
                GameObject skeldShieldLowerLeft = GameObject.Find("ShieldLowerLeft");
                skeldShieldLowerLeft.transform.SetParent(halconShields.transform);
                skeldShieldLowerLeft.transform.position = new Vector3(5.99f, -2.98f, skeldShieldLowerLeft.transform.position.z);
                GameObject skeldShieldsbulb = GameObject.Find("bulb");
                skeldShieldsbulb.transform.SetParent(halconShields.transform);
                skeldShieldsbulb.transform.position = new Vector3(9.55f, -1.05f, skeldShieldsbulb.transform.position.z);
                GameObject skeldShieldsbulbone = GameObject.Find("bulb (1)");
                skeldShieldsbulbone.transform.SetParent(halconShields.transform);
                skeldShieldsbulbone.transform.position = new Vector3(9.55f, -0.7f, skeldShieldsbulbone.transform.position.z);
                GameObject skeldShieldsbulbtwo = GameObject.Find("bulb (2)");
                skeldShieldsbulbtwo.transform.SetParent(halconShields.transform);
                skeldShieldsbulbtwo.transform.position = new Vector3(9.55f, -0.35f, skeldShieldsbulbtwo.transform.position.z);
                GameObject skeldShieldsbulbthree = GameObject.Find("bulb (3)");
                skeldShieldsbulbthree.transform.SetParent(halconShields.transform);
                skeldShieldsbulbthree.transform.position = new Vector3(5.45f, 0.15f, skeldShieldsbulbthree.transform.position.z);
                GameObject skeldShieldsbulbfour = GameObject.Find("bulb (4)");
                skeldShieldsbulbfour.transform.SetParent(halconShields.transform);
                skeldShieldsbulbfour.transform.position = new Vector3(5.75f, 0.3f, skeldShieldsbulbfour.transform.position.z);
                GameObject skeldShieldsbulbfive = GameObject.Find("bulb (5)");
                skeldShieldsbulbfive.transform.SetParent(halconShields.transform);
                skeldShieldsbulbfive.transform.position = new Vector3(6.05f, 0.45f, skeldShieldsbulbfive.transform.position.z);
                GameObject skeldShieldsbulbsix = GameObject.Find("bulb (6)");
                skeldShieldsbulbsix.transform.SetParent(halconShields.transform);
                skeldShieldsbulbsix.transform.position = new Vector3(6.35f, 0.6f, skeldShieldsbulbsix.transform.position.z);

                // Hallway objects
                GameObject halconHallway = senseiMap.transform.GetChild(8).transform.gameObject; // find halconBigHallway to be the parent
                GameObject skeldCrossHallwayFixWiringConsole = GameObject.Find("SkeldShip(Clone)/CrossHallway/FixWiringConsole");
                skeldCrossHallwayFixWiringConsole.transform.SetParent(halconHallway.transform);
                skeldCrossHallwayFixWiringConsole.transform.position = new Vector3(-8.9F, 4.93F, skeldCrossHallwayFixWiringConsole.transform.position.z);
                GameObject skeldBigYHallwayFixWiringConsole = GameObject.Find("SkeldShip(Clone)/BigYHallway/FixWiringConsole");
                skeldBigYHallwayFixWiringConsole.transform.SetParent(halconHallway.transform);
                skeldBigYHallwayFixWiringConsole.transform.position = new Vector3(4.685f, -12.53f, skeldBigYHallwayFixWiringConsole.transform.position.z);
                GameObject skeldAdminSurvCamera = GameObject.Find("SkeldShip(Clone)/AdminHallway/SurvCamera");
                skeldAdminSurvCamera.transform.SetParent(halconHallway.transform);
                skeldAdminSurvCamera.transform.position = new Vector3(5.345f, -12.45f, skeldAdminSurvCamera.transform.position.z);
                GameObject skeldBigHallwaySurvCamera = GameObject.Find("SkeldShip(Clone)/BigYHallway/SurvCamera");
                skeldBigHallwaySurvCamera.transform.SetParent(halconHallway.transform);
                skeldBigHallwaySurvCamera.transform.position = new Vector3(9.33f, 0.8f, skeldBigHallwaySurvCamera.transform.position.z);
                GameObject skeldNorthHallwaySurvCamera = GameObject.Find("SkeldShip(Clone)/NorthHallway/SurvCamera");
                skeldNorthHallwaySurvCamera.transform.SetParent(halconHallway.transform);
                skeldNorthHallwaySurvCamera.transform.position = new Vector3(-14.53f, -4.5f, skeldNorthHallwaySurvCamera.transform.position.z);
                GameObject skeldCrossHallwaySurvCamera = GameObject.Find("SkeldShip(Clone)/CrossHallway/SurvCamera");
                skeldCrossHallwaySurvCamera.transform.SetParent(halconHallway.transform);
                skeldCrossHallwaySurvCamera.transform.position = new Vector3(-9.85f, 4.75f, skeldCrossHallwaySurvCamera.transform.position.z);

                // Electrical objects
                GameObject halconElectrical = senseiMap.transform.GetChild(9).transform.gameObject; // find halconElectrical to be the parent
                GameObject skeldElecVent = GameObject.Find("ElecVent");
                skeldElecVent.transform.SetParent(halconElectrical.transform);
                skeldElecVent.transform.position = new Vector3(-5.22f, -13.95f, skeldElecVent.transform.position.z);
                GameObject skeldElecCalibrateConsole = GameObject.Find("CalibrateConsole");
                skeldElecCalibrateConsole.transform.SetParent(halconElectrical.transform);
                skeldElecCalibrateConsole.transform.position = new Vector3(-5.48f, -11.55f, skeldElecCalibrateConsole.transform.position.z);
                GameObject skeldelectric_frontset = GameObject.Find("electric_frontset");
                skeldelectric_frontset.transform.SetParent(halconElectrical.transform);
                skeldelectric_frontset.transform.position = new Vector3(-7.6f, -12.75f, skeldelectric_frontset.transform.position.z);
                GameObject skeldElecUploadDataConsole = GameObject.Find("SkeldShip(Clone)/Electrical/Ground/UploadDataConsole");
                skeldElecUploadDataConsole.transform.SetParent(halconElectrical.transform);
                skeldElecUploadDataConsole.transform.position = new Vector3(-7.75f, -8.25f, skeldElecUploadDataConsole.transform.position.z);
                GameObject skeldElecFixWiringConsole = GameObject.Find("SkeldShip(Clone)/Electrical/Ground/FixWiringConsole");
                skeldElecFixWiringConsole.transform.SetParent(halconElectrical.transform);
                skeldElecFixWiringConsole.transform.position = new Vector3(-6.37f, -8.725f, skeldElecFixWiringConsole.transform.position.z);
                GameObject skeldElectDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Electrical/Ground/DivertPowerConsole");
                skeldElectDivertPowerConsole.transform.SetParent(halconElectrical.transform);
                skeldElectDivertPowerConsole.transform.position = new Vector3(-8.55f, -11.25f, skeldElectDivertPowerConsole.transform.position.z);

                // Reactor objects
                GameObject halconReactor = senseiMap.transform.GetChild(10).transform.gameObject; // find halconReactor to be the parent
                GameObject skeldReactorVent = GameObject.Find("ReactorVent");
                skeldReactorVent.transform.SetParent(halconReactor.transform);
                skeldReactorVent.transform.position = new Vector3(-19.75f, -3.1f, skeldReactorVent.transform.position.z);
                GameObject skeldUpperReactorVent = GameObject.Find("UpperReactorVent");
                skeldUpperReactorVent.transform.SetParent(halconReactor.transform);
                skeldUpperReactorVent.transform.position = new Vector3(-19.75f, 0f, skeldUpperReactorVent.transform.position.z);
                GameObject skeldDivertPowerFalsePanel = GameObject.Find("DivertPowerFalsePanel");
                skeldDivertPowerFalsePanel.transform.SetParent(halconReactor.transform);
                skeldDivertPowerFalsePanel.transform.position = new Vector3(-18.6f, 1, skeldDivertPowerFalsePanel.transform.position.z);
                GameObject skeldreactor_toppipe = GameObject.Find("reactor_toppipe");
                skeldreactor_toppipe.transform.SetParent(halconReactor.transform);
                skeldreactor_toppipe.transform.position = new Vector3(-22.08f, 0.8f, skeldreactor_toppipe.transform.position.z);
                GameObject skeldreactor_base = GameObject.Find("reactor_base");
                skeldreactor_base.transform.SetParent(halconReactor.transform);
                skeldreactor_base.transform.position = new Vector3(-22.12f, -2.6f, skeldreactor_base.transform.position.z);
                GameObject skeldreactor_wireTop = GameObject.Find("reactor_wireTop");
                skeldreactor_wireTop.transform.SetParent(halconReactor.transform);
                skeldreactor_wireTop.transform.position = new Vector3(-21.21f, 0.175f, 6.7f);
                GameObject skeldreactor_wireBot = GameObject.Find("reactor_wireBot");
                skeldreactor_wireBot.transform.SetParent(halconReactor.transform);
                skeldreactor_wireBot.transform.position = new Vector3(-21.21f, -2.74f, 6.9f);
                skeldreactor_wireBot.transform.rotation = Quaternion.Euler(0f, 0f, 12.5f);

                // Storage objects
                GameObject halconStorage = senseiMap.transform.GetChild(11).transform.gameObject; // find halconStorage to be the parent
                GameObject skeldAirlockConsole = GameObject.Find("AirlockConsole");
                skeldAirlockConsole.transform.SetParent(halconStorage.transform);
                skeldAirlockConsole.transform.position = new Vector3(-9.725f, -12.6f, skeldAirlockConsole.transform.position.z);
                GameObject skeldstorage_Boxes = GameObject.Find("storage_Boxes");
                skeldstorage_Boxes.transform.SetParent(halconStorage.transform);
                skeldstorage_Boxes.transform.position = new Vector3(-13.55f, -10.4f, skeldstorage_Boxes.transform.position.z);
                GameObject skeldStorageFixWiringConsole = GameObject.Find("SkeldShip(Clone)/Storage/Ground/FixWiringConsole");
                skeldStorageFixWiringConsole.transform.SetParent(halconStorage.transform);
                skeldStorageFixWiringConsole.transform.position = new Vector3(-17.77f, -9.74f, skeldStorageFixWiringConsole.transform.position.z);

                // RightEngine objects
                GameObject halconRightEngine = senseiMap.transform.GetChild(12).transform.gameObject; // find halconRightEngine to be the parent
                GameObject skeldREngineVent = GameObject.Find("REngineVent");
                skeldREngineVent.transform.SetParent(halconRightEngine.transform);
                skeldREngineVent.transform.position = new Vector3(-18.9f, -8.7f, skeldREngineVent.transform.position.z);
                GameObject skeldRchain01 = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/chain01");
                skeldRchain01.transform.SetParent(halconRightEngine.transform);
                skeldRchain01.transform.position = new Vector3(-17.75f, -3.65f, skeldRchain01.transform.position.z);
                GameObject skeldRchain02 = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/chain02");
                skeldRchain02.transform.SetParent(halconRightEngine.transform);
                skeldRchain02.transform.position = new Vector3(-18.025f, -3.7f, skeldRchain02.transform.position.z);
                GameObject skeldRchain011 = GameObject.Find("chain01 (1)");
                skeldRchain011.transform.SetParent(halconRightEngine.transform);
                skeldRchain011.transform.position = new Vector3(-18.765f, -3.85f, skeldRchain011.transform.position.z);
                GameObject skeldREngineDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/DivertPowerConsole");
                skeldREngineDivertPowerConsole.transform.SetParent(halconRightEngine.transform);
                skeldREngineDivertPowerConsole.transform.position = new Vector3(-16.875f, -3.7f, skeldREngineDivertPowerConsole.transform.position.z);
                GameObject skeldREngineFuelEngineConsole = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/FuelEngineConsole");
                skeldREngineFuelEngineConsole.transform.SetParent(halconRightEngine.transform);
                skeldREngineFuelEngineConsole.transform.position = new Vector3(-19.65f, -7.12f, skeldREngineFuelEngineConsole.transform.position.z);
                GameObject skeldREngineAlignEngineConsole = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/AlignEngineConsole");
                skeldREngineAlignEngineConsole.transform.SetParent(halconRightEngine.transform);
                skeldREngineAlignEngineConsole.transform.position = new Vector3(-20.475f, -7.12f, skeldREngineAlignEngineConsole.transform.position.z);
                GameObject skeldREngineElectric = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/Electric");
                skeldREngineElectric.transform.SetParent(halconRightEngine.transform);
                skeldREngineElectric.transform.position = new Vector3(-19.2f, -5.475f, skeldREngineElectric.transform.position.z);
                GameObject skeldREngineSteam = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/Steam");
                skeldREngineSteam.transform.SetParent(halconRightEngine.transform);
                skeldREngineSteam.transform.position = new Vector3(-17.6f, -4.4f, skeldREngineSteam.transform.position.z);
                GameObject skeldREngineSteam1 = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/Steam (1)");
                skeldREngineSteam1.transform.SetParent(halconRightEngine.transform);
                skeldREngineSteam1.transform.position = new Vector3(-17.6f, -7.4f, skeldREngineSteam1.transform.position.z);
                GameObject skeldengineRight = GameObject.Find("engineRight");
                skeldengineRight.transform.SetParent(halconRightEngine.transform);
                skeldengineRight.transform.position = new Vector3(-19.02f, -5.982f, skeldengineRight.transform.position.z);

                // LeftEngine objects
                GameObject halconLeftEngine = senseiMap.transform.GetChild(13).transform.gameObject; // find halconLeftEngine to be the parent
                GameObject skeldLEngineVent = GameObject.Find("LEngineVent");
                skeldLEngineVent.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineVent.transform.position = new Vector3(-18.92f, 5.8f, skeldLEngineVent.transform.position.z);
                GameObject skeldLchain01 = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/chain01");
                skeldLchain01.transform.SetParent(halconLeftEngine.transform);
                skeldLchain01.transform.position = new Vector3(-17.1f, 6.1f, skeldLchain01.transform.position.z);
                GameObject skeldLchain02 = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/chain02");
                skeldLchain02.transform.SetParent(halconLeftEngine.transform);
                skeldLchain02.transform.position = new Vector3(-16.9f, 5.95f, skeldLchain02.transform.position.z);
                GameObject skeldLEngineDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/DivertPowerConsole");
                skeldLEngineDivertPowerConsole.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineDivertPowerConsole.transform.position = new Vector3(-18.92f, 6.95f, skeldLEngineDivertPowerConsole.transform.position.z);
                GameObject skeldLEngineFuelEngineConsole = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/engineLeft/FuelEngineConsole");
                skeldLEngineFuelEngineConsole.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineFuelEngineConsole.transform.position = new Vector3(-19.65f, 2.48f, skeldLEngineFuelEngineConsole.transform.position.z);
                GameObject skeldLEngineAlignEngineConsole = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/engineLeft/AlignEngineConsole");
                skeldLEngineAlignEngineConsole.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineAlignEngineConsole.transform.position = new Vector3(-20.375f, 2.56f, skeldLEngineAlignEngineConsole.transform.position.z);
                GameObject skeldLEngineElectric = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/engineLeft/Electric");
                skeldLEngineElectric.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineElectric.transform.position = new Vector3(-19.2f, 4.15f, skeldLEngineElectric.transform.position.z);
                GameObject skeldLEngineSteam = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/engineLeft/Steam");
                skeldLEngineSteam.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineSteam.transform.position = new Vector3(-17.6f, 5.1f, skeldLEngineSteam.transform.position.z);
                GameObject skeldLEngineSteam1 = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/engineLeft/Steam (1)");
                skeldLEngineSteam1.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineSteam1.transform.position = new Vector3(-17.7f, 3.8f, skeldLEngineSteam1.transform.position.z);
                GameObject skeldengineLeft = GameObject.Find("engineLeft");
                skeldengineLeft.transform.SetParent(halconLeftEngine.transform);
                skeldengineLeft.transform.position = new Vector3(-19.02f, 3.63f, skeldengineLeft.transform.position.z);

                // Comms objects
                GameObject halconComms = senseiMap.transform.GetChild(14).transform.gameObject; // find halconComms to be the parent
                GameObject skeldFixCommsConsole = GameObject.Find("FixCommsConsole");
                skeldFixCommsConsole.transform.SetParent(halconComms.transform);
                skeldFixCommsConsole.transform.position = new Vector3(7.555f, 3.34f, skeldFixCommsConsole.transform.position.z);
                skeldFixCommsConsole.GetComponent<SpriteRenderer>().sprite = CustomMain.customAssets.customComms.GetComponent<SpriteRenderer>().sprite;
                GameObject skeldcommsDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Comms/Ground/comms_wallstuff/DivertPowerConsole");
                skeldcommsDivertPowerConsole.transform.SetParent(halconComms.transform);
                skeldcommsDivertPowerConsole.transform.position = new Vector3(6.95f, 5.775f, skeldcommsDivertPowerConsole.transform.position.z);
                GameObject skeldcommsUploadDataConsole = GameObject.Find("SkeldShip(Clone)/Comms/Ground/comms_wallstuff/UploadDataConsole");
                skeldcommsUploadDataConsole.transform.SetParent(halconComms.transform);
                skeldcommsUploadDataConsole.transform.position = new Vector3(8.85f, 1.87f, skeldcommsUploadDataConsole.transform.position.z);
                GameObject skeldtapescomms_tapes0001 = GameObject.Find("tapes-comms_tapes0001");
                skeldtapescomms_tapes0001.transform.SetParent(halconComms.transform);
                skeldtapescomms_tapes0001.transform.position = new Vector3(6.047f, 5.8f, skeldtapescomms_tapes0001.transform.position.z);

                // Security objects
                GameObject halconSecurity = senseiMap.transform.GetChild(15).transform.gameObject; // find halconSecurity to be the parent
                GameObject skeldSecurityVent = GameObject.Find("SecurityVent");
                skeldSecurityVent.transform.SetParent(halconSecurity.transform);
                skeldSecurityVent.transform.position = new Vector3(-8.25f, 10.7f, skeldSecurityVent.transform.position.z);
                GameObject skeldmap_surveillance = GameObject.Find("map_surveillance");
                skeldmap_surveillance.transform.SetParent(halconSecurity.transform);
                skeldmap_surveillance.transform.position = new Vector3(-6.8f, 12.26f, skeldmap_surveillance.transform.position.z);
                GameObject skeldServers = GameObject.Find("Servers");
                skeldServers.transform.SetParent(halconSecurity.transform);
                skeldServers.transform.position = new Vector3(-8.5f, 11.72f, skeldServers.transform.position.z);
                GameObject skeldsecurityDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Security/Ground/DivertPowerConsole");
                skeldsecurityDivertPowerConsole.transform.SetParent(halconSecurity.transform);
                skeldsecurityDivertPowerConsole.transform.position = new Vector3(-5.3f, 12.025f, skeldsecurityDivertPowerConsole.transform.position.z);

                // Medical objects
                GameObject halconMedical = senseiMap.transform.GetChild(16).transform.gameObject; // find halconMedical to be the parent
                GameObject skeldMedVent = GameObject.Find("MedVent");
                skeldMedVent.transform.SetParent(halconMedical.transform);
                skeldMedVent.transform.position = new Vector3(-4.35f, -1.8f, skeldMedVent.transform.position.z);
                GameObject skeldMedScanner = GameObject.Find("MedScanner");
                skeldMedScanner.transform.SetParent(halconMedical.transform);
                skeldMedScanner.transform.position = new Vector3(-8.4f, -2.915f, skeldMedScanner.transform.position.z);
                GameObject skeldMedBayConsole = GameObject.Find("MedBayConsole");
                skeldMedBayConsole.transform.SetParent(halconMedical.transform);
                skeldMedBayConsole.transform.position = new Vector3(-4.315f, -0.595f, skeldMedBayConsole.transform.position.z);


                // Change original skeld map parent and hide the innecesary vanilla objects (don't destroy them, the game won't work otherwise)
                GameObject skeldship = GameObject.Find("SkeldShip(Clone)");
                Transform[] allChildren = skeldship.transform.GetComponentsInChildren<Transform>(true);
                for (int i = 1; i < allChildren.Length - 1; i++) {
                    allChildren[i].gameObject.SetActive(false);
                }
                skeldship.transform.SetParent(halconCollisions.transform);
                activatedSensei = true;
            }
        }
    }
}