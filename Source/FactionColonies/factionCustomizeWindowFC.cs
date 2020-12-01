using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RimWorld;
using Verse;
using RimWorld.Planet;

namespace FactionColonies
{
	public class factionCustomizeWindowFC : Window
	{

		public override Vector2 InitialSize
		{
			get
			{
				return new Vector2(445f, 280f);
			}
		}

		//declare variables

		//private int xspacing = 60;
		private int yspacing = 30;
		private int yoffset = 50;
		//private int headerSpacing = 30;
		private int length = 400;
		private int xoffset = 0;
		private int height = 200;


		private FactionFC faction;

		public string desc;
		public string header;

		private string name;
		private string title;
		private Texture2D tempFactionIcon;
		private string tempFactionIconPath;



		public factionCustomizeWindowFC(FactionFC faction)
		{
			this.forcePause = false;
			this.draggable = true;
			this.doCloseX = true;
			this.preventCameraMotion = false;
			this.faction = faction;
			this.header = "CustomizeFaction".Translate();
			this.name = faction.name;
			this.title = faction.title;

			this.tempFactionIcon = faction.factionIcon;
			this.tempFactionIconPath = faction.factionIconPath;
		}

		public override void PreOpen()
		{
			base.PreOpen();
		}

		public override void WindowUpdate()
		{
			base.WindowUpdate();
		}

		public override void OnAcceptKeyPressed()
		{
			base.OnAcceptKeyPressed();
			faction.title = title;
			faction.name = name;
			FactionColonies.getPlayerColonyFaction().Name = name;
			Find.World.GetComponent<FactionFC>().name = name;

		}

		public override void DoWindowContents(Rect inRect)
		{





			//grab before anchor/font
			GameFont fontBefore = Text.Font;
			TextAnchor anchorBefore = Text.Anchor;



			//Settlement Tax Collection Header
			Text.Anchor = TextAnchor.MiddleLeft;
			Text.Font = GameFont.Medium;
			Widgets.Label(new Rect(3, 3, 300, 60), header);


			Text.Font = GameFont.Small;
			for (int i = 0; i < 4; i++) //for each field to customize
			{
				switch (i)
				{
					case 0:  //faction name
						Widgets.Label(new Rect(xoffset+3, yoffset + yspacing * i, length / 4, yspacing), "FactionName".Translate() + ": ");
						name = Widgets.TextField(new Rect(xoffset + 3 + length / 4 + 5, yoffset + yspacing * i, length / 2, yspacing), name);
						break;

					case 1: //faction title
						Widgets.Label(new Rect(xoffset + 3, yoffset + yspacing * i, length / 4, yspacing), "FactionTitle".Translate() + ": ");
						title = Widgets.TextField(new Rect(xoffset + 3 + length / 4 + 5, yoffset + yspacing * i, length / 2, yspacing), title);
						break;

					case 2: //faction icon
						Widgets.Label(new Rect(xoffset + 3, yoffset + yspacing * i, length / 4, yspacing), "FactionIcon".Translate() + ": ");
						if(Widgets.ButtonImage(new Rect(xoffset + 3 + length / 4 + 5, yoffset + yspacing * i, 40, 40), tempFactionIcon))  //change to faction icon
						{
							List<FloatMenuOption> list = new List<FloatMenuOption>();
							foreach (KeyValuePair<string, Texture2D> pair in texLoad.factionIcons)
							{
								list.Add(new FloatMenuOption(pair.Key, delegate
								{
									tempFactionIcon = pair.Value;
									tempFactionIconPath = pair.Key;
								},pair.Value, Color.white));
							}
							FloatMenu menu = new FloatMenu(list);
							Find.WindowStack.Add(menu);

							//Messages.Message("ButtonNotAvailable".Translate() + ".", MessageTypeDefOf.CautionInput);
							//Log.Message("Faction icon select pressed");
							//Open window to select new icon
						}
						break;
					case 3:
						if (Widgets.ButtonTextSubtle(new Rect(xoffset + 3 + length / 4 + 5, yoffset + yspacing * i + 15, length / 2, yspacing), "AllowedRaces".Translate()))  //change to faction icon
						{
							List<string> races = new List<string>();
							List<FloatMenuOption> list = new List<FloatMenuOption>();
							list.Add(new FloatMenuOption("Enable All", delegate { faction.resetRaceFilter(); }));
							foreach (PawnKindDef def in DefDatabase<PawnKindDef>.AllDefsListForReading)
							{
								if (def.race.race.intelligence == Intelligence.Humanlike & races.Contains(def.race.label) == false && def.race.BaseMarketValue != 0)
								{
									if (def.race.label == "Human" && def.LabelCap != "Colonist")
									{

									}
									else
									{

										races.Add(def.race.label);
										list.Add(new FloatMenuOption(def.race.label.CapitalizeFirst() + " - Allowed: " + faction.raceFilter.Allows(def.race), delegate
										{
											if (faction.raceFilter.AllowedThingDefs.Count() == 1 && faction.raceFilter.Allows(def.race) == true)
											{
												Messages.Message("CannotHaveLessThanOneRace".Translate(), MessageTypeDefOf.RejectInput);
											}
											else if (faction.raceFilter.AllowedThingDefs.Count() > 1)
											{

												faction.raceFilter.SetAllow(def.race, !faction.raceFilter.Allows(def.race));
											} else
											{
												Log.Message("Empire Error - Zero races available for faction - Report this");
												Log.Message("Reseting race filter");
												faction.resetRaceFilter();
											}
										}));

									}
								}
							}
							FloatMenu menu = new FloatMenu(list);
							Find.WindowStack.Add(menu);

							//Messages.Message("ButtonNotAvailable".Translate() + ".", MessageTypeDefOf.CautionInput);
							//Log.Message("Faction icon select pressed");
							//Open window to select new icon
						}
						break;
				}
			}

			if(Widgets.ButtonText(new Rect((InitialSize.x-120-18)/2,yoffset + InitialSize.y - 120,120,30), "ConfirmChanges".Translate()))
			{
				Faction fact = FactionColonies.getPlayerColonyFaction();
				FactionFC component = Find.World.GetComponent<FactionFC>();
				faction.title = title;
				faction.name = name;
				fact.Name = name;
				component.name = name;
				component.factionIconPath = tempFactionIconPath;
				component.factionIcon = tempFactionIcon;
				component.updateFactionRaces();
				component.factionBackup = fact;
				
				faction.updateFactionIcon(ref fact, "FactionIcons/" + tempFactionIconPath);
				Find.LetterStack.ReceiveLetter("Note on Faction Icon", "Note: The faction icon on the world map will only update after a full restart of your game. Or pure luck.", LetterDefOf.NeutralEvent);
				Find.WindowStack.TryRemove(this);
			}

			//settlement buttons

			Text.Anchor = TextAnchor.UpperLeft;
			Text.Font = GameFont.Tiny;

			//0 tithe total string
			//1 source - -1
			//2 due/delivery date
			//3 Silver (- || +)
			//4 tithe


			Widgets.Label(new Rect(xoffset + 2, yoffset - yspacing + 2, length - 4, height - 4 + yspacing * 2), desc);
			Widgets.DrawBox(new Rect(xoffset, yoffset - yspacing, length, height - yspacing));

			//reset anchor/font
			Text.Font = fontBefore;
			Text.Anchor = anchorBefore;

		}



	}
}
