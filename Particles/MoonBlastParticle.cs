﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Particles
{
	public class MoonBlastParticle : Particle
	{
		int frame;
		int frameTick;
		public override void SetDefaults()
		{
			particle.width = 34;
			particle.height = 34;
			particle.scale = 1f;
			particle.timeLeft = 120;
			particle.oldPosLength = 10;
			particle.oldRotLength = 10;
			particle.SpawnAction = Spawn;
			particle.DeathAction = Death;
		}
		public override void AI()
		{
			for (int i = particle.oldPos.Length - 1; i > 0; i--)
			{
				particle.oldPos[i] = particle.oldPos[i - 1];
			}
			particle.oldPos[0] = particle.position;
			for (int i = particle.oldRot.Length - 1; i > 0; i--)
			{
				particle.oldRot[i] = particle.oldRot[i - 1];
			}
			particle.oldRot[0] = particle.rotation;
			if (particle.ai[0] == 0)
			{
				particle.ai[1] = Main.rand.NextFloat(2f, 8f) / 10f;
				particle.ai[2] = Main.rand.Next(0, 2);
				particle.ai[3] = Main.rand.NextFloat(0f, 360f);
				particle.timeLeft = (int)particle.ai[4] > 0 ? (int)particle.ai[4] : particle.timeLeft;
			}
			particle.ai[0]++;
			particle.rotation += Utils.Clamp(particle.velocity.X * 0.025f, -particle.ai[1], particle.ai[1]);
			particle.velocity *= 0.98f;
			//particle.velocity.Y += 0.03f * (particle.ai[2] == 0 ? -1 : 1);
			//particle.scale -= 0.01f;
			//particle.color = Color.Lerp(new Color(160, 16, 193), new Color(24, 18, 52), (360f - particle.TimeLeft) / 360f);
			particle.color = Color.Lerp(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0f), Color.Multiply(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0f), 0.5f), (360f - particle.timeLeft) / 360f);

			if (particle.scale <= 0f)
				particle.active = false;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D tex = GetTexture("ProvidenceMod/Particles/MoonBlastParticle");
			float alpha = particle.timeLeft <= 20 ? 1f - (1f / 20f * (20 - particle.timeLeft)) : 1f;
			if (alpha < 0f) alpha = 0f;
			float amount = MathHelper.Lerp(0f, 1f, ((Main.GlobalTime * 64f) % 360) / 360);
			Color hsl = Main.hslToRgb(amount, 1f, 0.75f);
			Color color = new Color((int)(hsl.R * alpha), (int)(hsl.G * alpha), (int)(hsl.B * alpha), 0);
			spriteBatch.Draw(GetTexture("ProvidenceMod/ExtraTextures/Flare"), particle.position - Main.screenPosition, new Rectangle(0, 0, 142, 42), color, Utils.AngleLerp(particle.ai[3].InRadians(), (particle.ai[3] + 90f).InRadians(), (120f - particle.timeLeft) / 120f), new Vector2(71, 21), 0.75f * particle.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(GetTexture("ProvidenceMod/ExtraTextures/Flare2"), particle.position - Main.screenPosition, new Rectangle(0, 0, 512, 512), color, Utils.AngleLerp(particle.ai[3].InRadians(), (particle.ai[3] + 90f).InRadians(), (120f - particle.timeLeft) / 120f), new Vector2(256, 256), 0.25f * particle.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(tex, particle.position - Main.screenPosition, tex.AnimationFrame(ref frame, ref frameTick, 4, 7, true), color, 0f, new Vector2(particle.width / 2, particle.height / 2), particle.scale + 0.5f, SpriteEffects.None, 0f);
			return false;
		}
		public void Spawn()
		{

		}
		public void Death()
		{

		}
	}
}