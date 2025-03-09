#pragma description ImHex Pattern Code
#pragma base_address 0x00

import type.leb128;

struct t_string {
    type::uLEB128 length [[name("Length"),comment("Number of character including null termination")]];
    char string[length] [[name("String"),comment("Array of characters making up the string")]];
};

// TODO: Change this to u4 using bitfields
enum e_packet : u8 {
    /* Network Packets */
    ACK, // Packet sent by the receiver to confirm that the previous message was received

    /* Chat Packets */
    CHAT_GLOBAL,
    CHAT_PRIVATE
};

/*              */
/* Data Structs */
/*              */

struct t_packet_chat_global {
    t_string message [[name("Message"),comment("String to be viewed by others")]];
} [[name("Data"),comment("Sent through global chat")]];

struct t_packet_chat_private {
    u8 id [[name("UID"),comment("Receiver's UID")]];
    t_string message [[name("Message"),comment("String to be viewed by others")]];
} [[name("Data"),comment("Sent through global chat")]];

/*                 */
/* Packet Elements */
/*                 */

struct t_header {
    e_packet type [[name("Type"),comment("Packet type")]];
};

struct t_format_packet {
    t_header header [[name("Header"),comment("Packet info")]];
    
    if         (header.type == e_packet::CHAT_GLOBAL) {
        t_packet_chat_global data;
    } else if (header.type == e_packet::CHAT_PRIVATE) {
        t_packet_chat_private data;
    }
};

t_format_packet packet @ 0x00 [[name("Packet"),comment("Byte array to be sent")]];