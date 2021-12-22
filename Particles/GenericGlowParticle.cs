﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ProvidenceMod.Particles
{
	public class GenericGlowParticle : Particle
	{
		public override void SetDefaults()
		{
			particle.width = 128;
			particle.height = 128;
			particle.timeLeft = 120;
			particle.tileCollide = false;
		}
		public override void AI()
		{
			particle.scale = (120 - particle.ai[0]) / 120; 
			particle.ai[0]++;
			particle.velocity *= 0.96f;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			spriteBatch.Draw(ModContent.GetTexture("ProvidenceMod/Particles/GenericGlowParticle"), particle.position - Main.screenPosition, new Rectangle(0, 0, 128, 128), particle.color, particle.rotation, new Vector2(64, 64), 0.125f * particle.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
