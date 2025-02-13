using System.Collections.Generic;
using UnityEngine;
using BepInEx.Configuration;
using System;
using System.Linq;
using HarmonyLib;
using Hazel;
using System.Reflection;
using System.Text;
using static LasMonjas.LasMonjas;
using LasMonjas.Core;

namespace LasMonjas
{
    public class CustomOptionHolder {
        public static string[] rates = new string[]{"0%", "100%"}; 
        public static string[] presets = new string[]{"Roles", "Capture the Flag", "Police and Thiefs", "King of the Hill", "Preset 5", "Preset 6", "Preset 7", "Preset 8", "Preset 9", "Preset 10" };

        // Game Options 
        public static CustomOption presetSelection;

        // Modifiers
        public static CustomOption removeSwipeCard;
        public static CustomOption removeAirshipDoors;
        public static CustomOption activateSenseiMap;
        public static CustomOption activateRoles; 
        public static CustomOption randomRoles; 
        public static CustomOption activateModifiers;
        public static CustomOption numberOfModifiers;
        public static CustomOption loverPlayer;
        public static CustomOption lighterPlayer;
        public static CustomOption blindPlayer;
        public static CustomOption flashPlayer;
        public static CustomOption bigchungusPlayer;
        public static CustomOption theChosenOnePlayer;
        public static CustomOption theChosenOneReportDelay;
        public static CustomOption performerPlayer;
        public static CustomOption performerDuration;

        // Capture the flag
        public static CustomOption captureTheFlagMode;
        public static CustomOption flagMatchDuration;
        public static CustomOption requiredFlags;
        public static CustomOption flagKillCooldown;
        public static CustomOption flagReviveTime;
        public static CustomOption flagInvincibilityTimeAfterRevive;

        // Police And Thief
        public static CustomOption policeAndThiefMode;
        public static CustomOption thiefModeMatchDuration;
        public static CustomOption thiefModerequiredJewels;
        public static CustomOption thiefModePoliceKillCooldown;
        public static CustomOption thiefModePoliceCanKillNearPrison;
        public static CustomOption thiefModePoliceCanSeeJewels;
        public static CustomOption thiefModePoliceCatchCooldown;
        public static CustomOption thiefModecaptureThiefTime;
        public static CustomOption thiefModepolicevision;
        public static CustomOption thiefModePoliceReviveTime;
        public static CustomOption thiefModeCanKill;
        public static CustomOption thiefModeKillCooldown;
        public static CustomOption thiefModeThiefReviveTime;
        public static CustomOption thiefModeInvincibilityTimeAfterRevive;

        // King of the hill
        public static CustomOption kingOfTheHillMode;
        public static CustomOption kingMatchDuration;
        public static CustomOption kingRequiredPoints;
        public static CustomOption kingCaptureCooldown;
        public static CustomOption kingKillCooldown;
        public static CustomOption kingCanKill;
        public static CustomOption kingReviveTime;
        public static CustomOption kingInvincibilityTimeAfterRevive;
        
        // Impostors configurable options

        // Mimic
        public static CustomOption mimicSpawnRate;
        public static CustomOption mimicCooldown;
        public static CustomOption mimicDuration;

        // Painter
        public static CustomOption painterSpawnRate;
        public static CustomOption painterCooldown;
        public static CustomOption painterDuration;

        // Demon
        public static CustomOption demonSpawnRate;
        public static CustomOption demonKillDelay;
        public static CustomOption demonCooldown;
        public static CustomOption demonCanKillNearNuns;

        // Janitor
        public static CustomOption janitorSpawnRate;
        public static CustomOption janitorCooldown;

        // Ilusionist
        public static CustomOption ilusionistSpawnRate;
        public static CustomOption ilusionistPlaceHatCooldown;
        public static CustomOption ilusionistLightsOutCooldown;
        public static CustomOption ilusionistLightsOutDuration;

        // Manipulator
        public static CustomOption manipulatorSpawnRate;
        public static CustomOption manipulatorCooldown;

        // Bomberman
        public static CustomOption bombermanSpawnRate;
        public static CustomOption bombermanBombCooldown;
        public static CustomOption bombermanBombDuration;

        // Chameleon
        public static CustomOption chameleonSpawnRate;
        public static CustomOption chameleonCooldown;
        public static CustomOption chameleonDuration;

        // Gambler
        public static CustomOption gamblerSpawnRate;
        public static CustomOption gamblerNumberOfShots;
        public static CustomOption gamblerCanCallEmergency;
        public static CustomOption gamblerCanShootMultipleTimes;
        public static CustomOption gamblerCanKillThroughShield;

        // Sorcerer
        public static CustomOption sorcererSpawnRate;
        public static CustomOption sorcererCooldown;
        public static CustomOption sorcererAdditionalCooldown;
        public static CustomOption sorcererSpellDuration;
        public static CustomOption sorcererCanCallEmergency;

        // Rebels configurable options

        // Renegade & Minion
        public static CustomOption renegadeSpawnRate;
        public static CustomOption renegadeKillCooldown;
        public static CustomOption renegadeCreateMinionCooldown;
        public static CustomOption renegadeCanUseVents;
        public static CustomOption renegadeCanRecruitMinion;    

        // Bountyhunter
        public static CustomOption bountyHunterSpawnRate;
        public static CustomOption bountyHunterCooldown;

        // Trapper
        public static CustomOption trapperSpawnRate;
        public static CustomOption trapperCooldown;
        public static CustomOption trapperMineNumber;
        public static CustomOption trapperMineDuration; 
        public static CustomOption trapperTrapNumber;
        public static CustomOption trapperTrapDuration;

        // Yinyanger
        public static CustomOption yinyangerSpawnRate;
        public static CustomOption yinyangerCooldown;

        // Challenger
        public static CustomOption challengerSpawnRate;
        public static CustomOption challengerCooldown; 
        

        // Neutral configurable options

        // Joker
        public static CustomOption jokerSpawnRate;
        public static CustomOption jokerCanSabotage;

        // Role Thief
        public static CustomOption rolethiefSpawnRate;
        public static CustomOption rolethiefCooldown;

        // Pyromaniac
        public static CustomOption pyromaniacSpawnRate;
        public static CustomOption pyromaniacCooldown;
        public static CustomOption pyromaniacDuration;

        // Treasure Hunter
        public static CustomOption treasureHunterSpawnRate;
        public static CustomOption treasureHunterCooldown;
        public static CustomOption treasureHunterTreasureNumber;
        public static CustomOption treasureHunterCanCallEmergency;

        // Devourer
        public static CustomOption devourerSpawnRate;
        public static CustomOption devourerCooldown;
        public static CustomOption devourerBodiesNumber;


        // Crewmates configurable options

        // Captain
        public static CustomOption captainSpawnRate;

        // Mechanic
        public static CustomOption mechanicSpawnRate;
        public static CustomOption mechanicNumberOfRepairs;

        // Sheriff
        public static CustomOption sheriffSpawnRate;
        public static CustomOption sheriffCooldown;
        public static CustomOption sheriffCanKillNeutrals;

        // Detective
        public static CustomOption detectiveSpawnRate;
        public static CustomOption detectiveShowFootprints;
        public static CustomOption detectiveCooldown;
        public static CustomOption detectiveShowFootPrintDuration;
        public static CustomOption detectiveAnonymousFootprints;
        public static CustomOption detectiveFootprintIntervall;
        public static CustomOption detectiveFootprintDuration;

        // Forensic
        public static CustomOption forensicSpawnRate;
        public static CustomOption forensicReportNameDuration;
        public static CustomOption forensicReportColorDuration;
        public static CustomOption forensicReportClueDuration;
        public static CustomOption forensicCooldown;
        public static CustomOption forensicDuration;
        public static CustomOption forensicOneTimeUse;

        // TimeTraveler
        public static CustomOption timeTravelerSpawnRate;
        public static CustomOption timeTravelerCooldown;
        public static CustomOption timeTravelerRewindTime;
        public static CustomOption timeTravelerShieldDuration;
        public static CustomOption timeTravelerReviveDuringRewind;

        // Squire
        public static CustomOption squireSpawnRate;
        public static CustomOption squireShowShielded;
        public static CustomOption squireShowAttemptToShielded;
        public static CustomOption squireResetShieldAfterMeeting;

        // Cheater
        public static CustomOption cheaterSpawnRate;
        public static CustomOption cheaterCanCallEmergency;
        public static CustomOption cheatercanOnlyCheatOthers;

        // FortuneTeller
        public static CustomOption fortuneTellerSpawnRate;
        public static CustomOption fortuneTellerCooldown;
        public static CustomOption fortuneTellerDuration; 
        public static CustomOption fortuneTellerNumberOfSee;
        public static CustomOption fortuneTellerKindOfInfo;
        public static CustomOption fortuneTellerPlayersWithNotification;

        // Hacker
        public static CustomOption hackerSpawnRate;
        public static CustomOption hackerCooldown;
        public static CustomOption hackerHackeringDuration;
        public static CustomOption hackerToolsNumber;
        public static CustomOption hackerRechargeTasksNumber;

        // Sleuth
        public static CustomOption sleuthSpawnRate;
        public static CustomOption sleuthUpdateIntervall;
        public static CustomOption sleuthResetTargetAfterMeeting;
        public static CustomOption sleuthCorpsesPathfindCooldown;
        public static CustomOption sleuthCorpsesPathfindDuration;

        // Fink
        public static CustomOption finkSpawnRate;
        public static CustomOption finkLeftTasksForImpostors;
        public static CustomOption finkIncludeTeamRenegade;
        public static CustomOption finkCooldown;
        public static CustomOption finkDuration;

        // Kid
        public static CustomOption kidSpawnRate;

        // Welder
        public static CustomOption welderSpawnRate;
        public static CustomOption welderCooldown;
        public static CustomOption welderTotalWelds;

        // Spiritualist
        public static CustomOption spiritualistSpawnRate;
        public static CustomOption spiritualistReviveTime;       

        // Vigilant
        public static CustomOption vigilantSpawnRate;
        public static CustomOption vigilantCooldown;
        public static CustomOption vigilantCameraNumber;
        public static CustomOption vigilantCamDuration;
        public static CustomOption vigilantCamMaxCharges;
        public static CustomOption vigilantCamRechargeTasksNumber;      
        
        // Hunter
        public static CustomOption hunterSpawnRate;
        public static CustomOption hunterResetTargetAfterMeeting;

        // Jinx
        public static CustomOption jinxSpawnRate;
        public static CustomOption jinxCooldown;
        public static CustomOption jinxJinxsNumber;

        // Coward
        public static CustomOption cowardSpawnRate;
        public static CustomOption cowardNumberOfCalls;
        
        // Medusa
        public static CustomOption medusaSpawnRate;
        public static CustomOption medusaCooldown;
        public static CustomOption medusaDelay;
        public static CustomOption medusaDuration;

        public static string cs(Color c, string s) {
            return string.Format("<color=#{0:X2}{1:X2}{2:X2}{3:X2}>{4}</color>", ToByte(c.r), ToByte(c.g), ToByte(c.b), ToByte(c.a), s);
        }
 
        private static byte ToByte(float f) {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }

        public static void Load() {
            
            // Game Options
            presetSelection = CustomOption.Create(0, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "Preset"), presets, null, true);

            // Capture the flag mode
            captureTheFlagMode = CustomOption.Create(50, cs(Sheriff.color, "Capture the Flag"), rates, null, true);
            flagMatchDuration = CustomOption.Create(51, cs(Sheriff.color, "Capture the Flag") + ": Match Duration", 180f, 180f, 300f, 30f, captureTheFlagMode);
            requiredFlags = CustomOption.Create(52, cs(Sheriff.color, "Capture the Flag") + ": Score Number", 3f, 3f, 5f, 1f, captureTheFlagMode);
            flagKillCooldown = CustomOption.Create(53, cs(Sheriff.color, "Capture the Flag") + ": Kill Cooldown", 10f, 5f, 15f, 1f, captureTheFlagMode);
            flagReviveTime = CustomOption.Create(54, cs(Sheriff.color, "Capture the Flag") + ": Revive Wait Time", 8f, 7f, 15f, 1f, captureTheFlagMode);
            flagInvincibilityTimeAfterRevive = CustomOption.Create(55, cs(Sheriff.color, "Capture the Flag") + ": Invincibility Time After Revive", 3f, 2f, 5f, 1f, captureTheFlagMode);

            // Police and Thief mode
            policeAndThiefMode = CustomOption.Create(60, cs(Coward.color, "Police and Thiefs"), rates, null, true);
            thiefModeMatchDuration = CustomOption.Create(61, cs(Coward.color, "Police and Thiefs") + ": Match Duration", 300f, 300f, 450f, 30f, policeAndThiefMode);
            thiefModerequiredJewels = CustomOption.Create(62, cs(Coward.color, "Police and Thiefs") + ": Jewel Number", 15f, 10f, 15f, 1f, policeAndThiefMode);
            thiefModePoliceKillCooldown = CustomOption.Create(63, cs(Coward.color, "Police and Thiefs") + ": Police Kill Cooldown", 15f, 10f, 20f, 1f, policeAndThiefMode);
            thiefModePoliceCanKillNearPrison = CustomOption.Create(64, cs(Coward.color, "Police and Thiefs") + ": Police can Kill near prison", false, policeAndThiefMode);
            thiefModePoliceCanSeeJewels = CustomOption.Create(65, cs(Coward.color, "Police and Thiefs") + ": Police can see Jewels", false, policeAndThiefMode);
            thiefModePoliceCatchCooldown = CustomOption.Create(66, cs(Coward.color, "Police and Thiefs") + ": Arrest Cooldown", 10f, 5f, 15f, 1f, policeAndThiefMode);
            thiefModecaptureThiefTime = CustomOption.Create(67, cs(Coward.color, "Police and Thiefs") + ": Time to Arrest", 3f, 3f, 5f, 1f, policeAndThiefMode);
            thiefModepolicevision = CustomOption.Create(68, cs(Coward.color, "Police and Thiefs") + ": Police vision range", 0.8f, 0.4f, 1.4f, 0.2f, policeAndThiefMode);
            thiefModePoliceReviveTime = CustomOption.Create(69, cs(Coward.color, "Police and Thiefs") + ": Police Revive Wait Time", 8f, 8f, 13f, 1f, policeAndThiefMode);
            thiefModeCanKill = CustomOption.Create(70, cs(Coward.color, "Police and Thiefs") + ": Thiefs Can Kill", false, policeAndThiefMode);
            thiefModeKillCooldown = CustomOption.Create(71, cs(Coward.color, "Police and Thiefs") + ": Thiefs Kill Cooldown", 25f, 20f, 30f, 1f, policeAndThiefMode);
            thiefModeThiefReviveTime = CustomOption.Create(72, cs(Coward.color, "Police and Thiefs") + ": Thiefs Revive Wait Time", 13f, 13f, 23f, 1f, policeAndThiefMode);
            thiefModeInvincibilityTimeAfterRevive = CustomOption.Create(73, cs(Coward.color, "Police and Thiefs") + ": Invincibility Time After Revive", 3f, 2f, 5f, 1f, policeAndThiefMode);

            // King of the hill mode
            kingOfTheHillMode = CustomOption.Create(80, cs(Squire.color, "King of the Hill"), rates, null, true);
            kingMatchDuration = CustomOption.Create(81, cs(Squire.color, "King of the Hill") + ": Match Duration", 180f, 180f, 300f, 30f, kingOfTheHillMode);
            kingRequiredPoints = CustomOption.Create(82, cs(Squire.color, "King of the Hill") + ": Score Number", 200f, 100f, 300f, 10f, kingOfTheHillMode);
            kingCaptureCooldown = CustomOption.Create(83, cs(Squire.color, "King of the Hill") + ": Capture Cooldown", 10f, 10f, 20f, 1f, kingOfTheHillMode);
            kingKillCooldown = CustomOption.Create(84, cs(Squire.color, "King of the Hill") + ": Kill Cooldown", 10f, 10f, 20f, 1f, kingOfTheHillMode);
            kingCanKill = CustomOption.Create(85, cs(Squire.color, "King of the Hill") + ": Kings can Kill", false, kingOfTheHillMode);
            kingReviveTime = CustomOption.Create(86, cs(Squire.color, "King of the Hill") + ": Revive Wait Time", 13f, 13f, 18f, 1f, kingOfTheHillMode);
            kingInvincibilityTimeAfterRevive = CustomOption.Create(87, cs(Squire.color, "King of the Hill") + ": Invincibility Time After Revive", 3f, 2f, 5f, 1f, kingOfTheHillMode);
            
            // Modifiers
            activateModifiers = CustomOption.Create(10, cs(Modifiers.color, "Modifiers"), rates, null, true);
            activateRoles = CustomOption.Create(11, cs(Modifiers.color, "Roles") + ": Activate mod roles and deactivate vanilla ones", true, activateModifiers);
            randomRoles = CustomOption.Create(12, cs(Modifiers.color, "Roles") + ": Role Assignment", new string[] { "Random", "List Order" }, activateModifiers);
            activateSenseiMap = CustomOption.Create(13, cs(Modifiers.color, "Activate Custom Skeld Map"), false, activateModifiers);
            removeSwipeCard = CustomOption.Create(14, cs(Modifiers.color, "Remove Swipe Card Task"), false, activateModifiers);
            removeAirshipDoors = CustomOption.Create(15, cs(Modifiers.color, "Remove Airship Doors"), false, activateModifiers);
            numberOfModifiers = CustomOption.Create(16, cs(Modifiers.color, "Modifiers ") + ": Modifiers Number", 7f, 1f, 7f, 1f, activateModifiers);
            loverPlayer = CustomOption.Create(17, cs(Modifiers.color, "Lovers") + ": Two players linked", false, activateModifiers);
            lighterPlayer = CustomOption.Create(18, cs(Modifiers.color, "Lighter") + ": Player with more Vision", false, activateModifiers);
            blindPlayer = CustomOption.Create(19, cs(Modifiers.color, "Blind") + ": Player with less Vision", false, activateModifiers);
            flashPlayer = CustomOption.Create(20, cs(Modifiers.color, "Flash") + ": Faster Player", false, activateModifiers);
            bigchungusPlayer = CustomOption.Create(21, cs(Modifiers.color, "Big Chungus") + ": Bigger and Slower Player", false, activateModifiers);
            theChosenOnePlayer = CustomOption.Create(22, cs(Modifiers.color, "The Chosen One") + ": Your killer will report your body", false, activateModifiers);
            theChosenOneReportDelay = CustomOption.Create(23, cs(Modifiers.color, "The Chosen One") + ": Report Delay", 0f, 0f, 5f, 1f, activateModifiers);
            performerPlayer = CustomOption.Create(24, cs(Modifiers.color, "Performer") + ": Your dead will trigger an alarm", false, activateModifiers);
            performerDuration = CustomOption.Create(25, cs(Modifiers.color, "Performer") + ": Alarm Duration", 20f, 10f, 30f, 1f, activateModifiers);

            // Mimic options
            mimicSpawnRate = CustomOption.Create(110, cs(Mimic.color, "Mimic"), rates, null, true);
            mimicCooldown = CustomOption.Create(111, cs(Mimic.color, "Mimic") + ": Cooldown", 30f, 20f, 40f, 2.5f, mimicSpawnRate);
            mimicDuration = CustomOption.Create(112, cs(Mimic.color, "Mimic") + ": Duration", 10f, 10f, 15f, 1f, mimicSpawnRate);

            // Painter options
            painterSpawnRate = CustomOption.Create(120, cs(Painter.color, "Painter"), rates, null, true);
            painterCooldown = CustomOption.Create(121, cs(Painter.color, "Painter") + ": Cooldown", 30f, 20f, 40f, 2.5f, painterSpawnRate);
            painterDuration = CustomOption.Create(122, cs(Painter.color, "Painter") + ": Duration", 10f, 10f, 15f, 1f, painterSpawnRate);

            // Demon options
            demonSpawnRate = CustomOption.Create(130, cs(Demon.color, "Demon"), rates, null, true);
            demonCooldown = CustomOption.Create(131, cs(Demon.color, "Demon") + ": Cooldown", 30f, 20f, 40f, 2.5f, demonSpawnRate);
            demonKillDelay = CustomOption.Create(132, cs(Demon.color, "Demon") + ": Delay Time", 10f, 10f, 15f, 1f, demonSpawnRate);
            demonCanKillNearNuns = CustomOption.Create(133, cs(Demon.color, "Demon") + ": Can Kill near Nuns", false, demonSpawnRate);

            // Janitor options
            janitorSpawnRate = CustomOption.Create(140, cs(Janitor.color, "Janitor"), rates, null, true);
            janitorCooldown = CustomOption.Create(141, cs(Janitor.color, "Janitor") + ": Cooldown", 30f, 20f, 40f, 2.5f, janitorSpawnRate);

            // Ilusionist options
            ilusionistSpawnRate = CustomOption.Create(150, cs(Ilusionist.color, "Ilusionist"), rates, null, true);
            ilusionistPlaceHatCooldown = CustomOption.Create(151, cs(Ilusionist.color, "Ilusionist") + ": Hats Cooldown", 20f, 15f, 30f, 1f, ilusionistSpawnRate);
            ilusionistLightsOutCooldown = CustomOption.Create(152, cs(Ilusionist.color, "Ilusionist") + ": Lights Cooldown", 30f, 20f, 40f, 1f, ilusionistSpawnRate);
            ilusionistLightsOutDuration = CustomOption.Create(153, cs(Ilusionist.color, "Ilusionist") + ": Blackout Duration", 10f, 5f, 15f, 1f, ilusionistSpawnRate);

            // Manipulator options
            manipulatorSpawnRate = CustomOption.Create(160, cs(Manipulator.color, "Manipulator"), rates, null, true);
            manipulatorCooldown = CustomOption.Create(161, cs(Manipulator.color, "Manipulator") + ": Cooldown", 30f, 20f, 40f, 2.5f, manipulatorSpawnRate);

            // Bomberman options
            bombermanSpawnRate = CustomOption.Create(170, cs(Bomberman.color, "Bomberman"), rates, null, true);
            bombermanBombCooldown = CustomOption.Create(171, cs(Bomberman.color, "Bomberman") + ": Cooldown", 60f, 40f, 60f, 5f, bombermanSpawnRate);
            bombermanBombDuration = CustomOption.Create(172, cs(Bomberman.color, "Bomberman") + ": Duration", 60f, 60f, 60f, 1f, bombermanSpawnRate);

            // Chameleon options
            chameleonSpawnRate = CustomOption.Create(180, cs(Chameleon.color, "Chameleon"), rates, null, true);
            chameleonCooldown = CustomOption.Create(181, cs(Chameleon.color, "Chameleon") + ": Cooldown", 30f, 20f, 40f, 2.5f, chameleonSpawnRate);
            chameleonDuration = CustomOption.Create(182, cs(Chameleon.color, "Chameleon") + ": Duration", 10f, 10f, 15f, 1f, chameleonSpawnRate);

            // Gambler options
            gamblerSpawnRate = CustomOption.Create(190, cs(Gambler.color, "Gambler"), rates, null, true);
            gamblerNumberOfShots = CustomOption.Create(191, cs(Gambler.color, "Gambler") + ": Shoot Number", 3f, 1f, 3f, 1f, gamblerSpawnRate);
            gamblerCanCallEmergency = CustomOption.Create(192, cs(Gambler.color, "Gambler") + ": Can use emergency button", false, gamblerSpawnRate);
            gamblerCanShootMultipleTimes = CustomOption.Create(193, cs(Gambler.color, "Gambler") + ": Can Shoot multiple times", true, gamblerSpawnRate);
            gamblerCanKillThroughShield = CustomOption.Create(194, cs(Gambler.color, "Gambler") + ": Ignore shields", false, gamblerSpawnRate);

            // Sorcerer Options
            sorcererSpawnRate = CustomOption.Create(200, cs(Sorcerer.color, "Sorcerer"), rates, null, true);
            sorcererCooldown = CustomOption.Create(201, cs(Sorcerer.color, "Sorcerer") + ": Cooldown", 30f, 20f, 40f, 2.5f, sorcererSpawnRate);
            sorcererAdditionalCooldown = CustomOption.Create(202, cs(Sorcerer.color, "Sorcerer") + ": Additional Cooldown per Spell", 5f, 5f, 10f, 1f, sorcererSpawnRate);
            sorcererSpellDuration = CustomOption.Create(204, cs(Sorcerer.color, "Sorcerer") + ": Spell Duration", 3f, 3f, 5f, 1f, sorcererSpawnRate);
            sorcererCanCallEmergency = CustomOption.Create(205, cs(Sorcerer.color, "Sorcerer") + ": Can use emergency button", false, sorcererSpawnRate);

            // Renegade & Minion options
            renegadeSpawnRate = CustomOption.Create(210, cs(Renegade.color, "Renegade"), rates, null, true);
            renegadeKillCooldown = CustomOption.Create(211, cs(Renegade.color, "Renegade") + ": Cooldown", 30f, 20f, 40f, 2.5f, renegadeSpawnRate);
            renegadeCreateMinionCooldown = CustomOption.Create(212, cs(Renegade.color, "Renegade") + ": Recruit Minion Cooldown", 10f, 10f, 20f, 2.5f, renegadeSpawnRate);
            renegadeCanUseVents = CustomOption.Create(213, cs(Renegade.color, "Renegade") + ": Can use Vents", true, renegadeSpawnRate);
            renegadeCanRecruitMinion = CustomOption.Create(214, cs(Renegade.color, "Renegade") + ": Can Recruit a Minion", true, renegadeSpawnRate);

            // Bountyhunter options
            bountyHunterSpawnRate = CustomOption.Create(220, cs(BountyHunter.color, "Bounty Hunter"), rates, null, true);
            bountyHunterCooldown = CustomOption.Create(221, cs(BountyHunter.color, "Bounty Hunter") + ": Cooldown", 20f, 10f, 20f, 2.5f, bountyHunterSpawnRate);

            // Trapper options
            trapperSpawnRate = CustomOption.Create(230, cs(Trapper.color, "Trapper"), rates, null, true);
            trapperCooldown = CustomOption.Create(231, cs(Trapper.color, "Trapper") + ": Cooldown", 20f, 20f, 30f, 1f, trapperSpawnRate);
            trapperMineNumber = CustomOption.Create(232, cs(Trapper.color, "Trapper") + ": Mine Number", 3f, 1f, 3f, 1f, trapperSpawnRate);
            trapperMineDuration = CustomOption.Create(233, cs(Trapper.color, "Trapper") + ": Mine Duration", 45f, 30f, 60f, 5f, trapperSpawnRate);
            trapperTrapNumber = CustomOption.Create(234, cs(Trapper.color, "Trapper") + ": Trap Number", 3f, 1f, 5f, 1f, trapperSpawnRate);
            trapperTrapDuration = CustomOption.Create(235, cs(Trapper.color, "Trapper") + ": Trap Duration", 60f, 30f, 120f, 5f, trapperSpawnRate);
            
            // Yinyanger options
            yinyangerSpawnRate = CustomOption.Create(240, cs(Yinyanger.color, "Yinyanger"), rates, null, true);
            yinyangerCooldown = CustomOption.Create(241, cs(Yinyanger.color, "Yinyanger") + ": Cooldown", 15f, 15f, 30f, 1f, yinyangerSpawnRate);

            // Challenger options
            challengerSpawnRate = CustomOption.Create(250, cs(Challenger.color, "Challenger"), rates, null, true);
            challengerCooldown = CustomOption.Create(251, cs(Challenger.color, "Challenger") + ": Cooldown", 20f, 20f, 30f, 1f, challengerSpawnRate); 
            
            // Joker options
            jokerSpawnRate = CustomOption.Create(260, cs(Joker.color, "Joker"), rates, null, true);
            jokerCanSabotage = CustomOption.Create(262, cs(Joker.color, "Joker") + ": Can Sabotage", true, jokerSpawnRate);

            // RoleThief options
            rolethiefSpawnRate = CustomOption.Create(270, cs(RoleThief.color, "Role Thief"), rates, null, true);
            rolethiefCooldown = CustomOption.Create(271, cs(RoleThief.color, "Role Thief") + ": Cooldown", 20f, 10f, 30f, 2.5f, rolethiefSpawnRate);

            // Pyromaniac options
            pyromaniacSpawnRate = CustomOption.Create(280, cs(Pyromaniac.color, "Pyromaniac"), rates, null, true);
            pyromaniacCooldown = CustomOption.Create(281, cs(Pyromaniac.color, "Pyromaniac") + ": Cooldown", 15f, 5f, 20f, 2.5f, pyromaniacSpawnRate);
            pyromaniacDuration = CustomOption.Create(282, cs(Pyromaniac.color, "Pyromaniac") + ": Ignite Duration", 3f, 1f, 5f, 1f, pyromaniacSpawnRate);

            // Treasure hunter options
            treasureHunterSpawnRate = CustomOption.Create(290, cs(TreasureHunter.color, "Treasure Hunter"), rates, null, true);
            treasureHunterCooldown = CustomOption.Create(291, cs(TreasureHunter.color, "Treasure Hunter") + ": Cooldown", 10f, 10f, 20f, 1f, treasureHunterSpawnRate);
            treasureHunterTreasureNumber = CustomOption.Create(292, cs(TreasureHunter.color, "Treasure Hunter") + ": Treasures to Win", 3f, 3f, 5f, 1f, treasureHunterSpawnRate);
            treasureHunterCanCallEmergency = CustomOption.Create(293, cs(TreasureHunter.color, "Treasure Hunter") + ": Can use emergency button", false, treasureHunterSpawnRate);

            // Devourer options
            devourerSpawnRate = CustomOption.Create(300, cs(Devourer.color, "Devourer"), rates, null, true);
            devourerCooldown = CustomOption.Create(301, cs(Devourer.color, "Devourer") + ": Cooldown", 20f, 15f, 20f, 1f, devourerSpawnRate);
            devourerBodiesNumber = CustomOption.Create(302, cs(Devourer.color, "Devourer") + ": Devours to Win", 3f, 2f, 5f, 1f, devourerSpawnRate);

            // Captain options
            captainSpawnRate = CustomOption.Create(310, cs(Captain.color, "Captain"), rates, null, true);

            // Mechanic options
            mechanicSpawnRate = CustomOption.Create(320, cs(Mechanic.color, "Mechanic"), rates, null, true);
            mechanicNumberOfRepairs = CustomOption.Create(321, cs(Mechanic.color, "Mechanic") + ": Repairs Number", 3f, 1f, 3f, 1f, mechanicSpawnRate);

            // Sheriff options
            sheriffSpawnRate = CustomOption.Create(330, cs(Sheriff.color, "Sheriff"), rates, null, true);
            sheriffCooldown = CustomOption.Create(331, cs(Sheriff.color, "Sheriff") + ": Cooldown", 30f, 20f, 40f, 2.5f, sheriffSpawnRate);
            sheriffCanKillNeutrals = CustomOption.Create(332, cs(Sheriff.color, "Sheriff") + ": Can Kill Neutrals", true, sheriffSpawnRate);

            // Detective options
            detectiveSpawnRate = CustomOption.Create(350, cs(Detective.color, "Detective"), rates, null, true);
            detectiveShowFootprints = CustomOption.Create(351, cs(Detective.color, "Detective") + ": Show Footprints", new string[] { "Button Use", "Always" }, detectiveSpawnRate);
            detectiveCooldown = CustomOption.Create(352, cs(Detective.color, "Detective") + ": Cooldown", 15f, 10f, 20f, 1f, detectiveSpawnRate);
            detectiveShowFootPrintDuration = CustomOption.Create(353, cs(Detective.color, "Detective") + ": Show footprints Duration", 10f, 10f, 15f, 1f, detectiveSpawnRate); 
            detectiveAnonymousFootprints = CustomOption.Create(354, cs(Detective.color, "Detective") + ": Anonymous Footprints", false, detectiveSpawnRate);
            detectiveFootprintIntervall = CustomOption.Create(355, cs(Detective.color, "Detective") + ": Footprints Interval", 0.5f, 0.25f, 2f, 0.25f, detectiveSpawnRate);
            detectiveFootprintDuration = CustomOption.Create(356, cs(Detective.color, "Detective") + ": Footprints Duration", 15f, 10f, 20f, 1f, detectiveSpawnRate);

            // Forensic options
            forensicSpawnRate = CustomOption.Create(360, cs(Forensic.color, "Forensic"), rates, null, true);
            forensicReportNameDuration = CustomOption.Create(361, cs(Forensic.color, "Forensic") + ": Time to know the name", 10, 2.5f, 10, 2.5f, forensicSpawnRate);
            forensicReportColorDuration = CustomOption.Create(362, cs(Forensic.color, "Forensic") + ": Time to know the color type", 20, 10, 20, 2.5f, forensicSpawnRate);
            forensicReportClueDuration = CustomOption.Create(363, cs(Forensic.color, "Forensic") + ": Time to know if the killer has hat, outfit, pet or visor", 30, 20, 30, 2.5f, forensicSpawnRate);
            forensicCooldown = CustomOption.Create(364, cs(Forensic.color, "Forensic") + ": Cooldown", 20f, 20f, 30f, 1f, forensicSpawnRate);
            forensicDuration = CustomOption.Create(365, cs(Forensic.color, "Forensic") + ": Question Duration", 5f, 5f, 10f, 1f, forensicSpawnRate);
            forensicOneTimeUse = CustomOption.Create(366, cs(Forensic.color, "Forensic") + ": One question per Ghost", true, forensicSpawnRate);

            // TimeTraveler options
            timeTravelerSpawnRate = CustomOption.Create(370, cs(TimeTraveler.color, "Time Traveler"), rates, null, true);
            timeTravelerCooldown = CustomOption.Create(371, cs(TimeTraveler.color, "Time Traveler") + ": Cooldown", 30f, 20f, 40f, 2.5f, timeTravelerSpawnRate);
            timeTravelerShieldDuration = CustomOption.Create(372, cs(TimeTraveler.color, "Time Traveler") + ": Shield Duration", 10f, 5f, 15f, 1f, timeTravelerSpawnRate);
            timeTravelerRewindTime = CustomOption.Create(373, cs(TimeTraveler.color, "Time Traveler") + ": Rewind Duration", 15f, 10f, 20f, 1f, timeTravelerSpawnRate);
            timeTravelerReviveDuringRewind = CustomOption.Create(374, cs(TimeTraveler.color, "Time Traveler") + ": Revive player during Rewind", true, timeTravelerSpawnRate);

            // Squire options
            squireSpawnRate = CustomOption.Create(380, cs(Squire.color, "Squire"), rates, null, true);
            squireShowShielded = CustomOption.Create(381, cs(Squire.color, "Squire") + ": Show Shielded Player to", new string[] { "Squire", "Both", "All" }, squireSpawnRate);
            squireShowAttemptToShielded = CustomOption.Create(382, cs(Squire.color, "Squire") + ": Play murder attempt sound if shielded", true, squireSpawnRate);
            squireResetShieldAfterMeeting = CustomOption.Create(383, cs(Squire.color, "Squire") + ": Can shield again after meeting", true, squireSpawnRate);

            // Cheater options
            cheaterSpawnRate = CustomOption.Create(390, cs(Cheater.color, "Cheater"), rates, null, true);
            cheaterCanCallEmergency = CustomOption.Create(391, cs(Cheater.color, "Cheater") + ": Can use emergency button", false, cheaterSpawnRate);
            cheatercanOnlyCheatOthers = CustomOption.Create(392, cs(Cheater.color, "Cheater") + ": Can swap himself", false, cheaterSpawnRate);

            // FortuneTeller options
            fortuneTellerSpawnRate = CustomOption.Create(400, cs(FortuneTeller.color, "Fortune Teller"), rates, null, true);
            fortuneTellerCooldown = CustomOption.Create(401, cs(FortuneTeller.color, "Fortune Teller") + ": Cooldown", 30f, 30f, 40f, 5f, fortuneTellerSpawnRate);
            fortuneTellerDuration = CustomOption.Create(402, cs(FortuneTeller.color, "Fortune Teller") + ": Reveal Time", 3f, 3f, 5f, 1f, fortuneTellerSpawnRate);
            fortuneTellerNumberOfSee = CustomOption.Create(403, cs(FortuneTeller.color, "Fortune Teller") + ": Reveal Number", 3f, 1f, 3f, 1f, fortuneTellerSpawnRate);
            fortuneTellerKindOfInfo = CustomOption.Create(404, cs(FortuneTeller.color, "Fortune Teller") + ": Revealed Information", new string[] { "Good / Bad", "Rol Name" }, fortuneTellerSpawnRate);
            fortuneTellerPlayersWithNotification = CustomOption.Create(405, cs(FortuneTeller.color, "Fortune Teller") + ": Show Notification to", new string[] { "Impostors", "Crewmates", "All", "Nobody" }, fortuneTellerSpawnRate);

            // Hacker options
            hackerSpawnRate = CustomOption.Create(410, cs(Hacker.color, "Hacker"), rates, null, true);
            hackerCooldown = CustomOption.Create(411, cs(Hacker.color, "Hacker") + ": Cooldown", 20f, 20f, 40f, 5f, hackerSpawnRate);
            hackerHackeringDuration = CustomOption.Create(412, cs(Hacker.color, "Hacker") + ": Duration", 15f, 10f, 15f, 1f, hackerSpawnRate);
            hackerToolsNumber = CustomOption.Create(413, cs(Hacker.color, "Hacker") + ": Battery Uses", 3f, 1f, 5f, 1f, hackerSpawnRate);
            hackerRechargeTasksNumber = CustomOption.Create(414, cs(Hacker.color, "Hacker") + ": Tasks for recharge batteries", 2f, 1f, 3f, 1f, hackerSpawnRate);

            // Sleuth options
            sleuthSpawnRate = CustomOption.Create(420, cs(Sleuth.color, "Sleuth"), rates, null, true);
            sleuthUpdateIntervall = CustomOption.Create(421, cs(Sleuth.color, "Sleuth") + ": Track Interval", 0f, 0f, 3f, 1f, sleuthSpawnRate);
            sleuthResetTargetAfterMeeting = CustomOption.Create(422, cs(Sleuth.color, "Sleuth") + ": Can Track again after meeting", true, sleuthSpawnRate);
            sleuthCorpsesPathfindCooldown = CustomOption.Create(424, cs(Sleuth.color, "Sleuth") + ": Track Corpses Cooldown", 30f, 20f, 40f, 2.5f, sleuthSpawnRate);
            sleuthCorpsesPathfindDuration = CustomOption.Create(425, cs(Sleuth.color, "Sleuth") + ": Track Corpses Duration", 10f, 5f, 15f, 2.5f, sleuthSpawnRate);

            // Fink options
            finkSpawnRate = CustomOption.Create(430, cs(Fink.color, "Fink"), rates, null, true);
            finkLeftTasksForImpostors = CustomOption.Create(431, cs(Fink.color, "Fink") + ": Tasks remaining for being revelead to Impostors", 1f, 1f, 3f, 1f, finkSpawnRate);
            finkIncludeTeamRenegade = CustomOption.Create(432, cs(Fink.color, "Fink") + ": Can reveal Renegade / Minion", true, finkSpawnRate);
            finkCooldown = CustomOption.Create(433, cs(Fink.color, "Fink") + ": Cooldown", 20f, 20f, 30f, 1f, finkSpawnRate);
            finkDuration = CustomOption.Create(434, cs(Fink.color, "Fink") + ": Hawk Eye Duration", 5f, 3f, 5f, 1f, finkSpawnRate);

            // Kid options
            kidSpawnRate = CustomOption.Create(440, cs(Kid.color, "Kid"), rates, null, true);

            // Welder options
            welderSpawnRate = CustomOption.Create(450, cs(Welder.color, "Welder"), rates, null, true);
            welderCooldown = CustomOption.Create(451, cs(Welder.color, "Welder") + ": Cooldown", 20f, 10f, 40f, 2.5f, welderSpawnRate);
            welderTotalWelds = CustomOption.Create(452, cs(Welder.color, "Welder") + ": Seal Number", 5f, 3f, 5f, 1f, welderSpawnRate);

            // Spiritualist options
            spiritualistSpawnRate = CustomOption.Create(460, cs(Spiritualist.color, "Spiritualist"), rates, null, true);
            spiritualistReviveTime = CustomOption.Create(461, cs(Spiritualist.color, "Spiritualist") + ": Revive Player Time", 10f, 10f, 15f, 1f, spiritualistSpawnRate);          

            // Vigilant options
            vigilantSpawnRate = CustomOption.Create(480, cs(Vigilant.color, "Vigilant"), rates, null, true);
            vigilantCooldown = CustomOption.Create(481, cs(Vigilant.color, "Vigilant") + ": Cooldown", 20f, 10f, 40f, 2.5f, vigilantSpawnRate);
            vigilantCameraNumber = CustomOption.Create(482, cs(Vigilant.color, "Vigilant") + ": Camera Number", 4f, 4f, 4f, 1f, vigilantSpawnRate);
            vigilantCamDuration = CustomOption.Create(483, cs(Vigilant.color, "Vigilant") + ": Remote Camera Duration", 10f, 5f, 15f, 1f, vigilantSpawnRate);
            vigilantCamMaxCharges = CustomOption.Create(484, cs(Vigilant.color, "Vigilant") + ": Battery Uses", 5f, 1f, 5f, 1f, vigilantSpawnRate);
            vigilantCamRechargeTasksNumber = CustomOption.Create(485, cs(Vigilant.color, "Vigilant") + ": Tasks for recharge batteries", 2f, 1f, 3f, 1f, vigilantSpawnRate);
                      
            // Hunter options
            hunterSpawnRate = CustomOption.Create(500, cs(Hunter.color, "Hunter"), rates, null, true);
            hunterResetTargetAfterMeeting = CustomOption.Create(501, cs(Hunter.color, "Hunter") + ": Can mark again after meeting", true, hunterSpawnRate);

            // Jinx
            jinxSpawnRate = CustomOption.Create(510, cs(Jinx.color, "Jinx"), rates, null, true);
            jinxCooldown = CustomOption.Create(515, cs(Jinx.color, "Jinx") + ": Cooldown", 20f, 10f, 40f, 2.5f, jinxSpawnRate);
            jinxJinxsNumber = CustomOption.Create(516, cs(Jinx.color, "Jinx") + ": Jinx Number", 10f, 5f, 15f, 1f, jinxSpawnRate);

            // Coward options
            cowardSpawnRate = CustomOption.Create(470, cs(Coward.color, "Coward"), rates, null, true);
            cowardNumberOfCalls = CustomOption.Create(471, cs(Coward.color, "Coward") + ": Number Of Meetings", 3f, 1f, 3f, 1f, cowardSpawnRate);
            
            // Medusa options
            medusaSpawnRate = CustomOption.Create(490, cs(Medusa.color, "Medusa"), rates, null, true);
            medusaCooldown = CustomOption.Create(491, cs(Medusa.color, "Medusa") + ": Cooldown", 20f, 15f, 30f, 2.5f, medusaSpawnRate);
            medusaDelay = CustomOption.Create(492, cs(Medusa.color, "Medusa") + ": Petrify Delay", 10f, 5f, 10f, 1f, medusaSpawnRate);
            medusaDuration = CustomOption.Create(493, cs(Medusa.color, "Medusa") + ": Duration", 10f, 5f, 10f, 1f, medusaSpawnRate);
        }
    }    
}