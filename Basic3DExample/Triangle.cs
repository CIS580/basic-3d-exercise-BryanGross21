using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Basic3DExample
{
	public class Triangle
	{
		/// <summary>
		/// The vertices of the triangle
		/// </summary>
		VertexPositionColor[] vertices;

		/// <summary>
		/// The effect used for rendering the triangle
		/// </summary>
		BasicEffect effect;

		/// <summary>
		/// The game this triangle belongs to 
		/// </summary>
		Game game;

		public Triangle(Game game) 
		{
			this.game = game;
			InitializeVertices();
			InitializeEffect();
		}

		void InitializeVertices() 
		{
			vertices = new VertexPositionColor[3];

			vertices[0].Position = new Vector3(0, 1, 0);
			vertices[0].Color = Color.Red;

			vertices[1].Position = new Vector3(1, 1, 0);
			vertices[1].Color = Color.Green;

			vertices[2].Position = new Vector3(1, 0, 0);
			vertices[2].Color = Color.Blue;
		}

		void InitializeEffect() 
		{
			effect = new BasicEffect(game.GraphicsDevice);
			effect.World = Matrix.Identity;
			effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 4), new Vector3(0, 0, 0), Vector3.Up);
			effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, game.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100f);
			effect.VertexColorEnabled = true;

		}

		/// <summary>
		/// Rotates the triangle around the y-axis
		/// </summary>
		/// <param name="gameTime">The GameTime object</param>
		public void Update(GameTime gameTime)
		{
			float angle = (float)gameTime.TotalGameTime.TotalSeconds;
			effect.World = Matrix.CreateRotationY(angle);
		}

		/// <summary>
		/// Draws the triangle
		/// </summary>
		public void Draw()
		{
			RasterizerState oldState = game.GraphicsDevice.RasterizerState;

			RasterizerState rasterizerState = new RasterizerState();
			rasterizerState.CullMode = CullMode.None;
			game.GraphicsDevice.RasterizerState = rasterizerState;



			effect.CurrentTechnique.Passes[0].Apply();
			game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
				PrimitiveType.TriangleList,
				vertices,       // The vertex data 
				0,              // The first vertex to use
				1               // The number of triangles to draw
			);

			game.GraphicsDevice.RasterizerState = oldState;
		}
	}
}
