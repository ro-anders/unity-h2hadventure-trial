using System;
namespace GameEngine
{
    public enum _COLOR
    {
        BLACK = 0,
        LTGRAY,
        WHITE,
        YELLOW,
        ORANGE,
        RED,
        PURPLE,
        BLUE,
        LTCYAN,
        CYAN,
        DKGREEN,
        LIMEGREEN,
        OLIVEGREEN,
        TAN,
        COPPER,
        JADE,
        CRYSTAL,
        DARK_CRYSTAL1,
        DARK_CRYSTAL2,
        DARK_CRYSTAL3,
        DARK_CRYSTAL4,
        FLASH
    }

    public class COLOR
    {
        public readonly int r;
        public readonly int g;
        public readonly int b;
        public COLOR(int r, int g, int b) {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public static COLOR table(_COLOR color)
        {
            return colorTable[(int)color];
        }

        public static COLOR table(int color)
        {
            return colorTable[color];
        }

        private static readonly COLOR[] colorTable = new COLOR[] {
            new COLOR(0x00,0x00,0x00), // black (0x0)
            new COLOR(0xcd,0xcd,0xcd), // light gray (0x08)
            new COLOR(0xff,0xff,0xff), // white (0x0e)
            new COLOR(0xFF,0xD8,0x4C), // yellow (0x1a)
            new COLOR(0xff,0x98,0x2c), // orange (0x28)
            new COLOR(0xfa,0x52,0x55), // red (0x36)
            new COLOR(0xA2,0x51,0xD9), // purple (0x66)
            new COLOR(0x6b,0x64,0xff), // blue (0x86)
            new COLOR(0x55,0xb6,0xff), // light cyan  (0x98)
            new COLOR(0x61,0xd0,0x70), // cyan  (0xa8)
            new COLOR(0x21,0xd9,0x1b), // dark green (0xb8)
            new COLOR(0x86,0xd9,0x22), // lime green (0xc8)
            new COLOR(0xa1,0xb0,0x34), // olive green (0xd8)
            new COLOR(0xd5,0xb5,0x43), // tan  (0xe8)
            new COLOR(0xcf,0x4d,0x0c), // copper
            new COLOR(0x00,0xa8,0x6b), // jade
            new COLOR(0xcc,0xcc,0xcc), // cystal
    // We successively darken the crystal to make is easier to see as they solve more riddles
            new COLOR(0xcb,0xcb,0xcb),
            new COLOR(0xca,0xca,0xca),
            new COLOR(0xc9,0xc9,0xc9),
            new COLOR(0xc8,0xc8,0xc8),
            new COLOR(0xa8,0xfc,0x41)  // flash (0xcb)
        };

    }

}
