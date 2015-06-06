create table Discussion (
    Id INTEGER PRIMARY KEY,
    Title TEXT,
	SubforumId INTEGER,
    CreatedOnUnix INTEGER,
	FOREIGN KEY(SubforumId) REFERENCES Subforum(Id)
);

create table ForumPost(
    Id INTEGER PRIMARY KEY,
    PostText TEXT,
	DiscussionId INTEGER,
    CreatedOnUnix INTEGER,
	FOREIGN KEY(DiscussionId) REFERENCES Discussion(Id)
);

create table Subforum(
	Id INTEGER PRIMARY KEY,
	Title TEXT,
	Description TEXT,
	CreatedOnUnix INTEGER
);

create table ForumUser(
	Id INTEGER PRIMARY KEY,
	Username TEXT,
	Password TEXT,
	CreatedOnUnix INTEGER
);


