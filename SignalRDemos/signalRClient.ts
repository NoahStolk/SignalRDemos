//     This code was generated by a Reinforced.Typings tool. 
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

export interface IUser {
	userId: string;
	firstName: string;
	lastName: string;
	avatar: string;
	color: string;
}
export interface ISimpleChatClientSendColor {
	userId: string;
	color: string;
}
export interface ISimpleChatClientSendMessage {
	userId: string;
	message: string;
}
export interface ISimpleChatClientSendAvatar {
	userId: string;
	avatar: string;
}
export interface ISimpleChatClientReceiveMessage {
	userId: string;
	message: string;
	dateTimeString: string;
}
export interface ISimpleChatClientReceiveUsers {
	users: IUser[];
}
