//     This code was generated by a Reinforced.Typings tool. 
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

export interface IUserSessionConnectionInfo {
	userId: string;
}
export interface ISimpleChatClientReceiveColors {
	colors: { [key: string]: string };
}
export interface ISimpleChatClientSendColor {
	userId: string;
	color: string;
}
export interface ISimpleChatClientReceiveMessage {
	userId: string;
	message: string;
}
export interface ISimpleChatClientSendMessage {
	userId: string;
	message: string;
}
