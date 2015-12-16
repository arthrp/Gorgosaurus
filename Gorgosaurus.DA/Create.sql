create table Discussion (
    Id INTEGER PRIMARY KEY,
    Title TEXT,
	SubforumId INTEGER NOT NULL CHECK(SubforumId > 0),
	ModifiedOnUnix INTEGER,
    CreatedOnUnix INTEGER,
	CreatedByUserId INTEGER NOT NULL CHECK(CreatedByUserId > 0),
	FOREIGN KEY(SubforumId) REFERENCES Subforum(Id),
	FOREIGN KEY(CreatedByUserId) REFERENCES ForumUser(Id)
);

create table ForumPost(
    Id INTEGER PRIMARY KEY,
    PostText TEXT,
	DiscussionId INTEGER NOT NULL CHECK(DiscussionId > 0),
	ModifiedOnUnix INTEGER,
    CreatedOnUnix INTEGER,
	CreatedByUserId INTEGER,
	FOREIGN KEY(DiscussionId) REFERENCES Discussion(Id),
	FOREIGN KEY(CreatedByUserId) REFERENCES ForumUser(Id)
);

create table Subforum(
	Id INTEGER PRIMARY KEY,
	Title TEXT UNIQUE,
	Description TEXT,
	ModifiedOnUnix INTEGER,
	CreatedOnUnix INTEGER,
	CreatedByUserId INTEGER
);

create table ForumUser(
	Id INTEGER PRIMARY KEY,
	Username TEXT UNIQUE,
	Password TEXT,
	Name TEXT,
	Surname TEXT,
	IsAdmin INTEGER DEFAULT 0 CHECK(IsAdmin in (0,1)),
	ModifiedOnUnix INTEGER,
	CreatedOnUnix INTEGER,
	CreatedByUserId INTEGER
);

create table ForumUserAdditionalInfo(
	Id INTEGER PRIMARY KEY,
	Name TEXT,
	Surname TEXT,
	ForumUserId INTEGER,
	FOREIGN KEY(ForumUserId) REFERENCES ForumUser(Id) ON DELETE CASCADE
);