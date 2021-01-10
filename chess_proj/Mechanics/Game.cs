using System;
using System.Threading.Tasks;
using chext.Discord.Parsing;
using chext.Discord;
using chext.Math;
using chext.Mechanics.Pieces;
using Discord.WebSocket;

#nullable enable

namespace chext.Mechanics
{
    public class Game
    {
        public readonly SocketTextChannel Channel;
        
        
        public Player White;
        public Player Black;
       
        private Renderer _renderer;
        private Board _board;
        private InGameParser _inGameParser;
        
        private static string CHEX_CHANNEL_NAME = "__chex__";

        public Game(SocketTextChannel channel, ulong blackPlayerId, ulong whitePlayerId)
        {
            Channel = channel;
            Black = new Player(blackPlayerId, false);
            White = new Player(whitePlayerId, true);
            
            _inGameParser = new InGameParser();
            _inGameParser.DisplayHandler += OnDisplayMoves;
            _inGameParser.MoveHandler    += OnMove;

            _board = new Board();
            _renderer = new Renderer(Channel!, _board);

        }

        public async void Init()
        {
            SetupStandard(_board);
            await _renderer.DrawBoard();
        }
        
        
        /// <summary>/// assumed to be channel in own channel, and message not from own chext bot client/// </summary>
        public void InChannelNonSelfMessageReceived(SocketMessage message)
        {
            Program.DebugLog("msg received game");
            _inGameParser.Parse(message, _board.Dimensions);
        }

        public void SetupStandard(Board board) //todo should be boards care
        {
            //SetupPawnRow(1, White);
            //SetupPawnRow(6, Black);
            SetupRowNonPawns(0, true);
            SetupRowNonPawns(7, false);
            
            board.SetCell(4, 4, new Rook(false));
            
            void SetupPawnRow(int rowIndex, bool isWhite)
            {
                for (int i = 0; i < _board.Dimensions; i++)
                    board.SetCell(rowIndex, i, new Pawn(isWhite));
            }
            
            void SetupRowNonPawns(int rowIndex, bool isWhite)
            {
                board.SetCell(rowIndex, 0, new Rook(isWhite));
                board.SetCell(rowIndex, 7, new Rook(isWhite));
                board.SetCell(rowIndex, 1, new Knight(isWhite));
                board.SetCell(rowIndex, 6, new Knight(isWhite));
                board.SetCell(rowIndex, 2, new Bishop(isWhite));
                board.SetCell(rowIndex, 5, new Bishop(isWhite));
                int queenIndex = isWhite ? 4 : 3;
                int kingIndex  = isWhite ? 3 : 4;
                board.SetCell(rowIndex, queenIndex, new King(isWhite));
                board.SetCell(rowIndex, kingIndex, new Queen(isWhite));
    
            }
        }
        
        
        private void OnDisplayMoves(Int2 position)
        {
            _renderer.DisplayMoves(position);
            _renderer.DrawBoard();
        }
        
        private void OnMove(Int2 from, Int2 to, bool isMoverWhite)
        {
            Player? mover;
            Player? opp;
            if (author.Id == Black.Id)
            {
                mover = Black;
                opp = White;
            }
            else if (author.Id == White.Id)
            {
                mover = White;
                opp = Black;
            }
            else
                return;
            
            if (author.Id != Black.Id)
            {
                Program.DebugLog("move attempt");
                _board.MovePiece(mover, opp, from, to);
                _renderer.DrawBoard();
            }
        }
    }
}