#pragma description Packet Binary Format
#pragma base_address 0x00
#pragma endian little
#include <type/leb128>

struct t_string {
    type::uLEB128 length [[name("Length"),comment("Number of character including null termination")]];
    char string[length] [[name("String"),comment("Array of characters making up the string")]];
};


struct t_uid {
    u8 id [[name("Identifier"),comment("1 Byte UID")]];
} [[name("UID8")]];

/*              */
/* Data Structs */
/*              */

struct t_packet_chat_global {
    t_string message [[name("Message"),comment("String to be viewed by others")]];
} [[name("Data"),comment("Sent through global chat")]];

struct t_packet_chat_private {
    t_uid id [[comment("Receiver's SUID")]];
    t_string message [[name("Message"),comment("String to be viewed by others")]];
} [[name("Data"),comment("Sent through global chat")]];

/*                 */
/* Packet Elements */
/*                 */

enum e_packet : u8 {
    /* Chat Packets 0010 */
    CHAT_GLOBAL = 0x20,
    CHAT_PRIVATE,
    
    /* Network Packets 0100 */
    ACK = 0x40, // Packet sent by the receiver to confirm that the previous message was received
    REFUSAL, // Packet sent by the server to refuse a request
} [[name("Type"),comment("Packet type")]];

struct t_signature {
    t_uid sender [[comment("Sender UID")]];
    u16 message_id [[name("Message Identifier"),comment("Number unique to the message")]];
} [[name("Signature"),comment("Sender info. and message ID")]];

struct t_header {
    e_packet type;
    t_signature sig;
} [[name("Header"),comment("Packet info")]];

struct t_format_packet {
    t_header header;
    
    if         (header.type == e_packet::CHAT_GLOBAL) {
        t_packet_chat_global data;
    } else if (header.type == e_packet::CHAT_PRIVATE) {
        t_packet_chat_private data;
    }
} [[name("Packet"),comment("Byte array to be sent")]];

t_format_packet packet @ 0x00;