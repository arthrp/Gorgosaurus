create table ForumPost(
    Id INTEGER PRIMARY KEY,
    PostText TEXT,
    CreatedOnUnix INTEGER
)

create table ForumThread (
    Id INTEGER PRIMARY KEY,
    Title TEXT,
    CreatedOnUnix INTEGER
)
