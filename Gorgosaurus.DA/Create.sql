create table Discussion (
    Id INTEGER PRIMARY KEY,
    Title TEXT,
	SubforumId INTEGER NOT NULL CHECK(SubforumId > 0),
	ModifiedOnUnix INTEGER,
    CreatedOnUnix INTEGER,
	FOREIGN KEY(SubforumId) REFERENCES Subforum(Id)
);

create table ForumPost(
    Id INTEGER PRIMARY KEY,
    PostText TEXT,
	DiscussionId INTEGER NOT NULL CHECK(DiscussionId > 0),
	Submitter_ForumUserId INTEGER NOT NULL CHECK(Submitter_ForumUserId > 0),
	ModifiedOnUnix INTEGER,
    CreatedOnUnix INTEGER,
	FOREIGN KEY(DiscussionId) REFERENCES Discussion(Id),
	FOREIGN KEY(Submitter_ForumUserId) REFERENCES ForumUser(Id)
);

create table Subforum(
	Id INTEGER PRIMARY KEY,
	Title TEXT,
	Description TEXT,
	ModifiedOnUnix INTEGER,
	CreatedOnUnix INTEGER
);

create table ForumUser(
	Id INTEGER PRIMARY KEY,
	Username TEXT,
	Password TEXT,
	IsAdmin INTEGER DEFAULT 0 CHECK(IsAdmin in (0,1)),
	ModifiedOnUnix INTEGER,
	CreatedOnUnix INTEGER
);


