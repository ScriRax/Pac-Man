namespace monJeu
{
    public static class Collision
    {
      public static bool IsTouchingLeft(Pacman player, Walls wall)
      {
          return player.PlayerRec.Right + player.Velocity.X > wall.WallRec.Left &&
            player.PlayerRec.Left < wall.WallRec.Left &&
            player.PlayerRec.Bottom > wall.WallRec.Top &&
            player.PlayerRec.Top < wall.WallRec.Bottom;
      }

      public static bool IsTouchingRight(Pacman player, Walls wall)
      {
          return player.PlayerRec.Left + player.Velocity.X < wall.WallRec.Right &&
            player.PlayerRec.Right > wall.WallRec.Right &&
            player.PlayerRec.Bottom > wall.WallRec.Top &&
            player.PlayerRec.Top < wall.WallRec.Bottom;
      }

      public static bool IsTouchingTop(Pacman player, Walls wall)
      {
          return player.PlayerRec.Bottom + player.Velocity.Y > wall.WallRec.Top &&
            player.PlayerRec.Top < wall.WallRec.Top &&
            player.PlayerRec.Right > wall.WallRec.Left &&
            player.PlayerRec.Left < wall.WallRec.Right;
      }

      public static bool IsTouchingBottom(Pacman player, Walls wall)
      {
          return player.PlayerRec.Top + player.Velocity.Y < wall.WallRec.Bottom &&
            player.PlayerRec.Bottom > wall.WallRec.Bottom &&
            player.PlayerRec.Right > wall.WallRec.Left &&
            player.PlayerRec.Left < wall.WallRec.Right;
      }
      public static bool IsPlayerTouchingGhostLeft(Pacman player, Ghost ghost)
      {
          return player.PlayerRec.Right + player.Velocity.X > ghost.GhostRec.Left &&
            player.PlayerRec.Left < ghost.GhostRec.Left &&
            player.PlayerRec.Bottom > ghost.GhostRec.Top &&
            player.PlayerRec.Top < ghost.GhostRec.Bottom;
      }

      public static bool IsPlayerTouchingGhostRight(Pacman player, Ghost ghost)
      {
          return player.PlayerRec.Left + player.Velocity.X < ghost.GhostRec.Right &&
            player.PlayerRec.Right > ghost.GhostRec.Right &&
            player.PlayerRec.Bottom > ghost.GhostRec.Top &&
            player.PlayerRec.Top < ghost.GhostRec.Bottom;
      }

      public static bool IsPlayerTouchingGhostTop(Pacman player, Ghost ghost)
      {
          return player.PlayerRec.Bottom + player.Velocity.Y > ghost.GhostRec.Top &&
            player.PlayerRec.Top < ghost.GhostRec.Top &&
            player.PlayerRec.Right > ghost.GhostRec.Left &&
            player.PlayerRec.Left < ghost.GhostRec.Right;
      }

      public static bool IsPlayerTouchingGhostBottom(Pacman player, Ghost ghost)
      {
          return player.PlayerRec.Top + player.Velocity.Y < ghost.GhostRec.Bottom &&
            player.PlayerRec.Bottom > ghost.GhostRec.Bottom &&
            player.PlayerRec.Right > ghost.GhostRec.Left &&
            player.PlayerRec.Left < ghost.GhostRec.Right;
      }
      public static bool IsGhostTouchingLeft(Walls wall, Ghost ghost)
      {
          return ghost.GhostRec.Right + ghost.Velocity.X > wall.WallRec.Left &&
            ghost.GhostRec.Left < wall.WallRec.Left &&
            ghost.GhostRec.Bottom > wall.WallRec.Top &&
            ghost.GhostRec.Top < wall.WallRec.Bottom;
      }

      public static bool IsGhostTouchingRight(Walls wall, Ghost ghost)
      {
          return ghost.GhostRec.Left + ghost.Velocity.X < wall.WallRec.Right &&
            ghost.GhostRec.Right > wall.WallRec.Right &&
            ghost.GhostRec.Bottom > wall.WallRec.Top &&
            ghost.GhostRec.Top < wall.WallRec.Bottom;
      }

      public static bool IsGhostTouchingTop(Walls wall, Ghost ghost)
      {
          return ghost.GhostRec.Bottom + ghost.Velocity.Y > wall.WallRec.Top &&
            ghost.GhostRec.Top < wall.WallRec.Top &&
            ghost.GhostRec.Right > wall.WallRec.Left &&
            ghost.GhostRec.Left < wall.WallRec.Right;
      }

      public static bool IsGhostTouchingBottom(Walls wall, Ghost ghost)
      {
          return ghost.GhostRec.Top + ghost.Velocity.Y < wall.WallRec.Bottom &&
            ghost.GhostRec.Bottom > wall.WallRec.Bottom &&
            ghost.GhostRec.Right > wall.WallRec.Left &&
            ghost.GhostRec.Left < wall.WallRec.Right;
      }

      public static bool IsGhostTouchingLeftInter(Intersection wall, Ghost ghost)
      {
          return ghost.GhostRec.Right + ghost.Velocity.X > wall.WallRec.Left + 25 &&
            ghost.GhostRec.Left < wall.WallRec.Left &&
            ghost.GhostRec.Bottom > wall.WallRec.Top &&
            ghost.GhostRec.Top < wall.WallRec.Bottom;
      }

      public static bool IsGhostTouchingRightInter(Intersection wall, Ghost ghost)
      {
          return ghost.GhostRec.Left + ghost.Velocity.X < wall.WallRec.Right - 25 &&
            ghost.GhostRec.Right > wall.WallRec.Right &&
            ghost.GhostRec.Bottom > wall.WallRec.Top &&
            ghost.GhostRec.Top < wall.WallRec.Bottom;
      }

      public static bool IsGhostTouchingTopInter(Intersection wall, Ghost ghost)
      {
          return ghost.GhostRec.Bottom + ghost.Velocity.Y > wall.WallRec.Top + 25 &&
            ghost.GhostRec.Top < wall.WallRec.Top &&
            ghost.GhostRec.Right > wall.WallRec.Left &&
            ghost.GhostRec.Left < wall.WallRec.Right;
      }

      public static bool IsGhostTouchingBottomInter(Intersection wall, Ghost ghost)
      {
          return ghost.GhostRec.Top + ghost.Velocity.Y < wall.WallRec.Bottom - 25 &&
            ghost.GhostRec.Bottom > wall.WallRec.Bottom &&
            ghost.GhostRec.Right > wall.WallRec.Left &&
            ghost.GhostRec.Left < wall.WallRec.Right;
      }

      public static bool IsTouchingCoinLeft(Pacman player, Coin coin)
      {
          return player.PlayerRec.Right + player.Velocity.X > coin.CoinRec.Left &&
            player.PlayerRec.Left < coin.CoinRec.Left &&
            player.PlayerRec.Bottom > coin.CoinRec.Top &&
            player.PlayerRec.Top < coin.CoinRec.Bottom;
      }

      public static bool IsTouchingCoinRight(Pacman player, Coin coin)
      {
          return player.PlayerRec.Left + player.Velocity.X < coin.CoinRec.Right &&
            player.PlayerRec.Right > coin.CoinRec.Right &&
            player.PlayerRec.Bottom > coin.CoinRec.Top &&
            player.PlayerRec.Top < coin.CoinRec.Bottom;
      }

      public static bool IsTouchingCoinTop(Pacman player, Coin coin)
      {
          return player.PlayerRec.Bottom + player.Velocity.Y > coin.CoinRec.Top &&
            player.PlayerRec.Top < coin.CoinRec.Top &&
            player.PlayerRec.Right > coin.CoinRec.Left &&
            player.PlayerRec.Left < coin.CoinRec.Right;
      }

      public static bool IsTouchingCoinBottom(Pacman player, Coin coin)
      {
          return player.PlayerRec.Top + player.Velocity.Y < coin.CoinRec.Bottom &&
            player.PlayerRec.Bottom > coin.CoinRec.Bottom &&
            player.PlayerRec.Right > coin.CoinRec.Left &&
            player.PlayerRec.Left < coin.CoinRec.Right;
      }
    }
}