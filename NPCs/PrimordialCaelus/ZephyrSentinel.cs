﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Dusts;
using ProvidenceMod.Projectiles.Boss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ProvidenceMod.Projectiles.ProvidenceGlobalProjectileAI;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.NPCs.PrimordialCaelus
{
	public class ZephyrSentinel : ModNPC
	{
		public int frame;
		public int frameTick;
		public float rotation;
		public Rectangle rect = new Rectangle(0, 0, 118, 118);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Sentinel");
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 118;
			npc.height = 118;
			npc.Opacity = 0f;
			npc.damage = 25;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.lifeMax = 200;
			npc.townNPC = false;
			npc.scale = 1f;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.chaseable = true;
			npc.knockBackResist = 0f;
		}
		public override void AI()
		{
			npc.position = Main.npc[(int)npc.ai[1]].Center + new Vector2(128f, 0f).RotatedBy(npc.ai[2]).RotatedBy(rotation.InRadians());
			rotation += 4;
			if (npc.ai[0] < 60)
			{
				npc.ai[0]++;
				npc.Opacity += 1f / 60f;
			}
			if (npc.ai[0] >= 60)
			{
				npc.Opacity = 1f;
				if (npc.ai[0] % 120 == 0)
				{
					for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver4)
					{
						Vector2 speed = new Vector2(0f, 4f).RotatedBy(i);
						Dust.NewDustPerfect(npc.Center, DustType<CloudDust>(), speed);
						Dust.NewDustPerfect(npc.Center, DustType<CloudDust>(), speed.RotatedBy(i / 2f));
						Dust.NewDustPerfect(npc.Center, DustType<CloudDust>(), speed.RotatedBy(i / -2f));
					}
					Projectile.NewProjectile(npc.Center, new Vector2(10f, 0f).RotatedBy(npc.AngleTo(npc.ClosestPlayer().position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Normal, 1);
				}
				npc.ai[0]++;
			}
			if (npc.ai[0] == 600)
			{
				npc.life = 0;
				HitEffect(0, 0);
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/PrimordialCaelus/ZephyrSentinelSheet"), npc.position - Main.screenPosition, npc.AnimationFrame(ref frame, ref frameTick, 5, 20, true), new Color(npc.Opacity, npc.Opacity, npc.Opacity, npc.Opacity), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			return false;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life > 0)
				return;
			for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver4)
			{
				Vector2 speed = new Vector2(0f, 4f).RotatedBy(i);
				Dust.NewDustPerfect(npc.Center, DustType<CloudDust>(), speed, default, default, 5f);
				Dust.NewDustPerfect(npc.Center, DustType<CloudDust>(), speed.RotatedBy(i / 2f), default, default, 5f);
				Dust.NewDustPerfect(npc.Center, DustType<CloudDust>(), speed.RotatedBy(i / -2f), default, default, 5f);
			}
			if (ProvidenceWorld.lament && !ProvidenceWorld.wrath)
				for (int i = 0; i < 8; i += 2)
					Projectile.NewProjectile(npc.Center, new Vector2(0f, 10f).RotatedBy(i * MathHelper.PiOver4), ProjectileType<SentinelShard>(), 25, 0f, default, i);
			if(ProvidenceWorld.wrath)
				for (int i = 0; i < 8; i++)
					Projectile.NewProjectile(npc.Center, new Vector2(0f, 10f).RotatedBy(i * MathHelper.PiOver4), ProjectileType<SentinelShard>(), 25, 0f, default, i);
		}
	}
}
