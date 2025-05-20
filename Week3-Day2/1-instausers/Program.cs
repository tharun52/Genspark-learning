// 1) Design a C# console app that uses a jagged array to store data for Instagram posts by multiple users. Each user can have a different number of posts, 
// and each post stores a caption and number of likes.

// You have N users, and each user can have M posts (varies per user).

// Each post has:

// A caption (string)

// A number of likes (int)

// Store this in a jagged array, where each index represents one user's list of posts.

// Display all posts grouped by user.

// No file/database needed — console input/output only.

// Example output
// Enter number of users: 2

// User 1: How many posts? 2
// Enter caption for post 1: Sunset at beach
// Enter likes: 150
// Enter caption for post 2: Coffee time
// Enter likes: 89

// User 2: How many posts? 1
// Enter caption for post 1: Hiking adventure
// Enter likes: 230

// --- Displaying Instagram Posts ---
// User 1:
// Post 1 - Caption: Sunset at beach | Likes: 150
// Post 2 - Caption: Coffee time | Likes: 89

// User 2:
// Post 1 - Caption: Hiking adventure | Likes: 230


// Test case
// | User | Number of Posts | Post Captions        | Likes      |
// | ---- | --------------- | -------------------- | ---------- |
// | 1    | 2               | "Lunch", "Road Trip" | 40, 120    |
// | 2    | 1               | "Workout"            | 75         |
// | 3    | 3               | "Book", "Tea", "Cat" | 30, 15, 60 |

internal class Program
{
    class InstagramPost
    {
        public string Caption { get; set; }
        public int Likes { get; set; }

        public InstagramPost(string caption, int likes)
        {
            Caption = caption;
            Likes = likes;
        }
    }


    static int GetValidInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value) && value > 0)
            {
                break;
            }
            else
                Console.WriteLine("Invalid Input. Please Try Again");
        }
        return value;
    }

    static string GetValidCaption(string prompt)
    {
        string caption;
        while (true)
        {
            Console.Write(prompt);
            caption = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(caption))
                break;
            else
                Console.WriteLine("Caption cannot be empty. Please enter a valid caption.");
        }
        return caption;
    }

    static void DisplayPosts(InstagramPost[][] userPosts)
    {
        Console.WriteLine("\n--- Displaying Instagram Posts ---");
        for (int i = 0; i < userPosts.Length; i++)
        {
            Console.WriteLine($"\nUser {i + 1} : ");
            for (int j = 0; j < userPosts[i].Length; j++)
            {
                Console.WriteLine($"Post {j + 1} - Caption: {userPosts[i][j].Caption} | Likes: {userPosts[i][j].Likes}");
            }
            Console.WriteLine();
        }
    }

    static void Main()
    {
        int no_of_users = GetValidInt("Enter Number of Users : ");

        InstagramPost[][] userPosts = new InstagramPost[no_of_users][];

        for (int i = 0; i < no_of_users; i++)
        {
            Console.WriteLine($"\nUser{i + 1} : ");
            int no_of_posts = GetValidInt("How many posts ? : ");

            userPosts[i] = new InstagramPost[no_of_posts];

            for (int j = 0; j < no_of_posts; j++)
            {
                string caption = GetValidCaption($"Enter caption for post {j + 1} : ");
                int likes = GetValidInt("Enter likes : ");

                userPosts[i][j] = new InstagramPost(caption, likes);
            }
        }
        DisplayPosts(userPosts);
    }

}
