Demo for "REST by design" talk.

As an API client I need to be able to use these API:

Accounts:
GET /clients/123/accounts
{
	links:[],
	accounts:[
		{
			name:"Smart Access",
			number:111111,
			balance:10500,
			type:"Savings",
			openOn:20122012,
			isCard:false
		},
		{...}
	]
}
GET /clients/123/transactions?skip=10&take=10
GET /clients/123/transactions?fields=name,number,balance


Transactions:
GET /clients/123/transactions
{
	links:[],
	accounts:[
		{
			date: 12122013,
			amount: 100,
			type: "Debit"
		},
		{...}
	]
}

GET /clients/123/transactions?skip=10&take=10
GET /clietns/123/transactions?dateFrom=31122013&dateTo=31032014&amountFrom=5000


Transfers between customer's accounts:
POST /clients/123/payments
{
	"accountFrom": 111111,
	"accountTo": 222222,
	"amount": 450,
	"description": "for rent"
}

SmartTags:
GET /clients/123/smarttags
{
	smarttags:[
		{
			id: 123456789,
			ordered: 12122014,
			active:false,
			deleted:false
		}
	]
}

GET /clients/123/smarttags/123456789
{
	smarttag:{
			id: 123456789,
			ordered: 12122014,
			active:false,
			deleted:false
	}
}

PATCH /clients/123/smarttags/123456789
{
	active:true
}

DELETE /clients/123/smarttags/123456789
{
	deleted:true
}


Tasks:
- do implement interfaces (mapping, patch with Delta<>, fields, transaction filter, pagination)
- implement JSend via MsgHandler
- cors?





