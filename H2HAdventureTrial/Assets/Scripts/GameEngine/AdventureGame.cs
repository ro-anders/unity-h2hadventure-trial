using System;
using System.Collections;
using System.Collections.Generic;

namespace GameEngine
{

    public class AdventureGame
    {
        private const int ADVENTURE_SCREEN_WIDTH = Adv.ADVENTURE_SCREEN_WIDTH;
        private const int ADVENTURE_SCREEN_HEIGHT = Adv.ADVENTURE_SCREEN_HEIGHT;
        private const int ADVENTURE_OVERSCAN = Adv.ADVENTURE_OVERSCAN;
        private const int ADVENTURE_TOTAL_SCREEN_HEIGHT = Adv.ADVENTURE_TOTAL_SCREEN_HEIGHT;
        private const double ADVENTURE_FRAME_PERIOD = Adv.ADVENTURE_FRAME_PERIOD;
        private const int ADVENTURE_MAX_NAME_LENGTH = Adv.ADVENTURE_MAX_NAME_LENGTH;

        private AdventureView view;

        public AdventureGame(AdventureView inView) {
            view = inView;
        }

        public void Adventure_Run()
        {
            PrintDisplay();
        }

        public void PrintDisplay()
        {
            //// get the playfield data
            //int displayedRoom = (displayWinningRoom ? winningRoom : objectBall->displayedRoom);
            //const ROOM* currentRoom = roomDefs[displayedRoom];
            //const byte* roomData = currentRoom->graphicsData;
            byte[] roomData = new byte[] {
                0xF0,0xFE,0x15,      // XXXXXXXXXXX X X X      R R R RRRRRRRRRRR
                0x30,0x03,0x1F,      // XX        XXXXXXX      RRRRRRR        RR
                0x30,0x03,0xFF,      // XX        XXXXXXXXXXRRRRRRRRRR        RR
                0x30,0x00,0xFF,      // XX          XXXXXXXXRRRRRRRR          RR
                0x30,0x00,0x3F,      // XX          XXXXXX    RRRRRR          RR
                0x30,0x00,0x00,      // XX                                    RR
                0xF0,0xFF,0x0F       // XXXXXXXXXXXXXX            RRRRRRRRRRRRRR
            };

            //// get the playfield color
            //COLOR color = ((gameState == GAMESTATE_WIN) && (winFlashTimer > 0)) ? GetFlashColor() : colorTable[currentRoom->color];
            COLOR color = COLOR.table(_COLOR.YELLOW);
            COLOR colorBackground = COLOR.table(_COLOR.LTGRAY);

            // Fill the entire backbuffer with the playfield background color before we draw anything else
            view.Platform_PaintPixel(colorBackground.r, colorBackground.g, colorBackground.b, 0, 0, ADVENTURE_SCREEN_WIDTH, ADVENTURE_TOTAL_SCREEN_HEIGHT);

            //// paint the surround under the playfield layer
            //for (int ctr = 0; ctr < numPlayers; ++ctr)
            //{
            //    if ((surrounds[ctr]->room == displayedRoom) && (surrounds[ctr]->state == 0))
            //    {
            //        DrawObject(surrounds[ctr]);
            //    }
            //}
            //// get the playfield mirror flag
            //bool mirror = currentRoom->flags & ROOMFLAG_MIRROR;
            bool mirror = false;
                
            //
            // Extract the playfield register bits and paint the playfield
            // The playfied register is 20 bits wide encoded across 3 bytes
            // as follows:
            //    PF0   |  PF1   |  PF2
            //  xxxx4567|76543210|01234567
            // Each set bit indicates playfield color - else background color -
            // the size of each block is 8 x 32, and the drawing is shifted
            // upwards by 16 pixels
            //

            // mask values for playfield bits
                byte[] shiftreg = new byte[20] {
                0x10,0x20,0x40,0x80,
                0x80,0x40,0x20,0x10,0x8,0x4,0x2,0x1,
                0x1,0x2,0x4,0x8,0x10,0x20,0x40,0x80
            };

            // each cell is 8 x 32
            const int cell_width = 8;
            const int cell_height = 32;


            // draw the playfield
            for (int cy = 0; cy <= 6; cy++)
            {
                byte pf0 = roomData[(cy * 3) + 0];
                byte pf1 = roomData[(cy * 3) + 1];
                byte pf2 = roomData[(cy * 3) + 2];

                int ypos = 6 - cy;

                for (int cx = 0; cx < 20; cx++)
                {
                    int bit = 0;

                    if (cx < 4)
                        bit = pf0 & shiftreg[cx];
                    else if (cx < 12)
                        bit = pf1 & shiftreg[cx];
                    else
                        bit = pf2 & shiftreg[cx];

                    if (bit != 0)
                    {
                        view.Platform_PaintPixel(color.r, color.g, color.b, cx * cell_width, ypos * cell_height, cell_width, cell_height);
                        if (mirror)
                            view.Platform_PaintPixel(color.r, color.g, color.b, (cx + 20) * cell_width, ypos * cell_height, cell_width, cell_height);
                        else
                            view.Platform_PaintPixel(color.r, color.g, color.b, ((40 - (cx + 1)) * cell_width), ypos * cell_height, cell_width, cell_height);
                    }
                }
            }

            ////
            //// Draw the balls
            ////
            //COLOR defaultColor = colorTable[roomDefs[displayedRoom]->color];

            //for (int i = 0; i < numPlayers; ++i)
            //{
            //    BALL* player = gameBoard->getPlayer(i);
            //    if (player->displayedRoom == displayedRoom)
            //    {
            //        COLOR ballColor = (player->isGlowing() ? GetFlashColor() : defaultColor);
            //        DrawBall(gameBoard->getPlayer(i), ballColor);
            //    }
            //}

            ////
            //// Draw any objects in the room
            ////
            //DrawObjects(displayedRoom);


        }

    }
}