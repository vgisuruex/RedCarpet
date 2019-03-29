using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedCarpet
{
    public partial class StageSelectForm : Form
    {
        string[] LevelsList;
        public StageSelectForm()
        {
            InitializeComponent();

            string[] FileList = Directory.GetFiles(Properties.Settings.Default.GamePath + "StageData/", "*.szs"); // get all files from /StageData that ends with szs
            List<string> levels = new List<string>();
            foreach (string path in FileList) // getting filePaths and do the following for every file
            {
                if (path.Contains("Map")) // like it says, if it contains "Map" in it
                {
                    string filename = Path.GetFileNameWithoutExtension(path); // create variable "filename" with the filename
                    levels.Add(filename);
                    StageSelectListBox.Items.Add(filename);
                }
            }
            LevelsList = levels.ToArray();
        }

        private void StageSelectListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form1 form = (Form1)this.Owner; // get the owner of this form

            // check if there is an item under the mouse pointer
            int index = this.StageSelectListBox.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                form.LoadLevel(Properties.Settings.Default.GamePath + "StageData/" + StageSelectListBox.Items[index] + ".szs"); // load the name from /StageData/ and add .szs file extension
                this.Close(); // let the window disappear
            }
        }

        private void TextSearch_TextChanged(object sender, EventArgs e)
        {
            if (TextSearch.Text.Trim() == "" && StageSelectListBox.Items.Count == LevelsList.Length) return;
            StageSelectListBox.Items.Clear();
            if (TextSearch.Text.Trim() == "") StageSelectListBox.Items.AddRange(LevelsList);
            foreach (string s in LevelsList) if (s.IndexOf(TextSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0) StageSelectListBox.Items.Add(s);
        }

        bool SeachPlaceholder = true;
        private void TextSearch_click(object sender, EventArgs e)
        {
            if (!SeachPlaceholder) return;
            TextSearch.Text = "";
            SeachPlaceholder = false;
        }

        private void StageSelectForm_Load(object sender, EventArgs e)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("SuperMario3dWorldFont.otf");
            comboBox1.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            listBox1.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //WORLDS
            if (comboBox1.SelectedIndex == 0)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("CourseSelectW1ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CourseSelectW2ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CourseSelectW3ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CourseSelectW4ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CourseSelectW5ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CourseSelectW6ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CourseSelectW7ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CourseSelectW8ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CourseSelectS1ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CourseSelectStageMap1.szs"));
            }
            //1
            if (comboBox1.SelectedIndex == 1)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("EnterCatMarioStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GoldRoomKouraZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("NokonokoCaveStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("AoCoinFirstZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ClimbMountainStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CloudBonusEZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DownRiverStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("FlipCircusBonusRoomZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("FlipCircusStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperBullLv1StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KoopaChaseLv1StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoWorldClear01StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioBrigadeTentenStageMap1.szs"));
            }
            //2
            if (comboBox1.SelectedIndex == 2)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("GoldRoomWaterfallZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SideWaveDesertStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TouchAndMikeBonusRoomZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TouchAndMikeStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TouchAndMikeSecondZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CloudBonusTouchAndMikeZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ShadowTunnelStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("RotateFieldDoremiZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("RotateFieldStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("RotateFieldBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("RotateFieldGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DoubleMarioFieldStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperKuribonLv1StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoWorldClear02StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryHouseEnemyZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryHouseEnemyStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KillerTankStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TankBunbun01ZoneMap1.szs"));
            }
            //3
            if (comboBox1.SelectedIndex == 3)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("SnowBallParkStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ClimbWirenetStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TeresaConveyorStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ShortGardenStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CloudBonusCZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GoldRoomUnderSeaZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DokanAquariumStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DokanAquariumBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DokanAquariumCZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DokanAquariumDZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DokanAquariumGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DashRidgeStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DashRidgeGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TruckWaterfallStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperKameckLv1StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperTentackLv1StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossTentackZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoWorldClear03StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioBrigadeWaterStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TrainPunpun01ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KillerExpressStageMap1.szs"));
            }
            //4
            if (comboBox1.SelectedIndex == 4)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("CrawlerHillDokanRoomZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CrawlerHillStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("PipePackunDenBonusRoomZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("PipePackunDenStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("PipePackunDenFirstZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("PipePackunDenSecondZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("PipePackunDenThirdZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("PipePackunDenGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChikaChikaBoomerangAoCoinZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChikaChikaBoomerangStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChikaChikaBoomerangAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChikaChikaBoomerangBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChikaChikaBoomerangCZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TrampolineHighlandStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GabonMountainBonusRoomZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GabonMountainStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperGorobonLv1StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperFireBrosLv1StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossGorobonStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossGorobonZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoWorldClear04StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryHouseDashStageMap1.szs"));
            }
            //5
            if (comboBox1.SelectedIndex == 5)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("NokonokoBeachStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TimerCoinRoomCircusZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SwingCircusBonusZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SwingCircusStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ShortMultiLiftStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SavannaRockStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BombCaveStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CloudBonusRoomZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("JumpFlipSweetsStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SneakingLightStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperBullLv2StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperFireBrosLv2StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossWackunFortressFirstZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossWackunFortressStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossWackunFortressBossZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoWorldClear05StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioBrigadeTeresaStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GoldenExpressStageMap1.szs"));
            }
            //6
            if (comboBox1.SelectedIndex == 6)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("RouteDokanTourStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("WeavingShipStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("WeavingShip00ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("WeavingShip01ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("WeavingShip02ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("WeavingShipGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KarakuriCastleStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("JungleCruiseStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BlastSnowFieldStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ClimbFortressStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChorobonTowerStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperKyupponLv1StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperFireBrosLv3StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperBossBunretsuLv1StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossBunretsuZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoWorldClear06StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryHouseBallStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TankBunbun02ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BombTankStageMap1.szs"));
            }
            //CASTLE
            if (comboBox1.SelectedIndex == 7)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperGorobonLv2StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CastleGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GKCastleGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoCourseStartCastleStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("FireBrosFortressBonusZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("FireBrosFortressStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DarkFlipPanelStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ShortAmidaStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DonketsuArrowStepBonusRoomZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DonketsuArrowStepStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DonketsuArrowStepGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ZigzagBuildingStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SyumockSpotStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SyumockSpotAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SyumockSpotBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SyumockSpotCZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SyumockSpotDZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("SyumockSpotGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("RagingMagmaStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("RagingMagmaAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperKyupponLv2StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperFireBrosLv4StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KoopaChaseLv2StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KoopaChaseLv2ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoKoopaCatchFairyAgainStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioBrigadeConveyorStageMap1.szs"));
            }
            //BOWSER
            if (comboBox1.SelectedIndex == 8)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("ClimbMysteryBoxAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("NeedleBridgeStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DownDesertStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GearSweetsStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("EchoRoadStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("WaterElevatorCaveStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CloudBonusWaterElevatorCaveZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DarknessHauntedHouseStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GotogotonValleyStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperBossBunretsuLv2StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossBunretsu02ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperTentackLv2StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossTentack02ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KoopaLastStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KoopaLastAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KoopaLastBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryHouseClimbStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryClimbAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryClimbBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryClimbCZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryClimbDZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryClimbEZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryClimbFZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("EnemyExpressStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TrainPunpun02ZoneMap1.szs"));
            }
            //STAR
            if (comboBox1.SelectedIndex == 9)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("RainbowRoadBonusZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("RainbowRoadStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("CloudBonusZeldaZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GalaxyRoadStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoEndRollRosettaStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("WheelMysteryBoxZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("WheelCanyonStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GoalPoleRunawayStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BlockLandStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("HexScrollStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GiantAoCoinZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GiantUnderGroundStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TerenFogGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TerenFogStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TerenFogBridgeZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TerenFogMysteryZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BoxKillerStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioBrigadeRotateRoomStageMap1.szs"));
            }
            //MUSHROOM
            if (comboBox1.SelectedIndex == 10)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeRotateFieldDoremiZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeRotateFieldStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeRotateFieldBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeRotateFieldGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeClimbMountainDokanZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeClimbMountainStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeJungleCruiseStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeShadowTunnelStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeKarakuriCastleStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeWeavingShip00ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeWeavingShipStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeWeavingShip02ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeWeavingShipGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeDonketsuArrowStepStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeDonketsuArrowStepZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeMysteryHouseEnemyStageMap1.szs"));
            }
            //FLOWER
            if (comboBox1.SelectedIndex == 11)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeFlipCircusBonusRoomZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeFlipCircusStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeHexScrollStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeNeedleBridgeStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TankBunbun03ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossTentack03ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("BossBunretsu03ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TrainPunpun03ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeBossParadeStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeChorobonTowerStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangePipePackunDenStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangePipePackunDenFirstZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangePipePackunDenThirdZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangePipePackunDenGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeFireBrosFortressStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeSavannaRockStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeTeresaConveorStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeDokanAquariumStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeAquariumAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeAquariumBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeAquariumCZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeChikaChikaBoomerangStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeChikaAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeChikaBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeChikaCZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ArrangeNokonokoBeachStageMap1.szs"));
            }
            //CROWN
            if (comboBox1.SelectedIndex == 12)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChampionshipStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChampionshipAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChampionshipBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChampionshipCZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChampionshipDZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChampionshipFZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("ChampionshipGoalZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryHouseMaxStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxBlastSnowFieldZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxBlockLandZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxBossGorobonZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxClimbFortressZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxDashRidgeZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxDonketsuArrowStepZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxEchoRoadZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxFlipCircusZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxMysteryBallAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxMysteryBallCZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxMysteryBallDZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxMysteryClimbBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxMysteryClimbEZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxMysteryClimbFZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxMysteryDashGZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxMysteryDashIZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxMysteryDashJZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxMysteryEnemyZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxNeedleBridgeZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxNokonokoCaveZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxPipePackunDenZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxSneakingLightZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxTeresaConveyorZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxTrampolineHighlandZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryMaxWheelCanyonZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioBrigadeInfernoStageMap1.szs"));
            }
            // MISC
            if (comboBox1.SelectedIndex == 13)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(LevelDatabase.GetLevelName("CommonTitleDemoZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TitleDemo00StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TitleDemo01StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TitleDemo02StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TitleDemo03StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TitleDemo04StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("TitleDemo05StageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioHouseBlueZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioHouseLv1BlueStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioHouseLv2BlueStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioHouseLv3BlueStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioHouseLavaZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioHouseLv3LavaStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioHouseLv3NightStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioHouseNightZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioHouseInsideZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioHouseLv1InsideStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DoremiRoomBZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoOpeningStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoOpeningExitStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoOpeningDokanStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("RouletteRoomZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("FairyHouseZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("FairyHouseLavaStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("FairyHouseNightStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("FairyHouseBlueStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("FairyHouseInsideStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoGameOverStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoCourseStartTrainGoldStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoEndRollStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoEndRollEndStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoCourseStartKoopaCastleLastStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperInsideZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("GateKeeperOutsideZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryHouseEntranceZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryHouseEntranceLv2ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("MysteryHouseEntranceLv3ZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("KinopioRoomAZoneMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoCourseStartTrainEnemyStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoCourseStartTankBombStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoCourseStartTrainStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoCourseStartTankStageMap1.szs"));
                listBox1.Items.Add(LevelDatabase.GetLevelName("DemoCourseStartKoopaCastleStageMap1.szs"));
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form1 form = (Form1)this.Owner;

            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                form.LoadLevel(Properties.Settings.Default.GamePath + "StageData/" + LevelDatabase.GetLevelFileName(listBox1.Items[index].ToString())); // load the name from /StageData/ and add .szs file extension
                this.Close(); // let the window disappear
            }
        }
    }
}
