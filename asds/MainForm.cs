using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Media;

namespace asds
{
    public partial class MainForm : Form
    {
    	private int progressCount;
        public int score = 0;
        public int health =3;
        private int highScore = 0;
        public SoundPlayer song = new SoundPlayer("Song.wav");
        public MainForm()
        {
            InitializeComponent();
            song.Play();
            pictureBox4.Visible = false; // show three hearts
                pictureBox3.Visible = false; // Hide two hearts
                pictureBox2.Visible = false; // Hide one heart
                pictureBox1.Visible = false;  // Hide no heart
            health = 3;
            LoadHighScore();
            score = 0;
               HighlightClosestLabel(GetClosestLabel());
            label3.Text = TruthValues();
            label1.Text = Equations();
            label8.Text= Equations2();
            label9.Text= Equations3();
            label7.Text = "HighScore: "+highScore;
            label8.Visible=false;
            label9.Visible=false;
             label1.Visible=false;
             button1.Visible=false;
             button2.Visible=false;
             progressBar1.Visible=false;
             label3.Visible=false;
               label7.Visible = false;
           label5.Visible = false;

             
        }
        	void Button8Click(object sender, EventArgs e)
		{
        		pictureBox4.Visible = true; // show three hearts
                pictureBox3.Visible = false; // Hide two hearts
                pictureBox2.Visible = false; // Hide one heart
                pictureBox1.Visible = false;  // Hide no heart
        		panel2.Visible = false;
        		 progressBar1.Visible=true;
        		button1.Visible=true;
             button2.Visible=true;
        		label1.Visible=true;
        	timer1.Enabled = true;
			timer1.Start();
             timer2.Start();
              timer3.Start();
              button8.Visible = false;
              label3.Visible = true;
           label7.Visible = true;
           label5.Visible = true;
           score = 0;
		}

        
        
        
        
        
        
       private Label GetClosestLabel()
{
    Label[] specificLabels = { label1, label8, label9 };
    Label closestLabel = null;
    int closestDistance = int.MaxValue; // Initialize with the maximum value

    // Iterate through the specificLabels array to find the label closest to the threshold
    foreach (var label in specificLabels)
    {
        // Check if the label is visible before considering it
        if (label.Visible)
        {
            int distance = Math.Abs(label.Top + label.Height / 2 - 450);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestLabel = label;
            }
        }
    }

    // Highlight the closest label

    return closestLabel;
}
        private const string HighScoreFilePath = "highscore.txt";
        
        private void LoadHighScore()
        {
            if (File.Exists(HighScoreFilePath))
            {
                try
                {
                    string scoreStr = File.ReadAllText(HighScoreFilePath);
                    highScore = int.Parse(scoreStr);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading high score: " + ex.Message);
                }
            }
        }

        private void SaveHighScore()
        {
            try
            {
                File.WriteAllText(HighScoreFilePath, highScore.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving high score: " + ex.Message);
            }
        }


       private string TruthValues()
{
    Random rnd = new Random();
    bool[] truthValues = new bool[4];

    // Generate random truth values for each proposition
    for (int i = 0; i < truthValues.Length; i++)
    {
        truthValues[i] = rnd.Next(2) == 0; // 0 represents false, 1 represents true
    }

    // Check if all values are the same, and if so, change one of them
    if (truthValues.All(value => value == truthValues[0]))
    {
        truthValues[rnd.Next(truthValues.Length)] = !truthValues[0]; // Change one value
    }

    // Format the truth values
    string formattedValues = string.Format("p={0} q={1} r={2} s={3}",
                                            truthValues[0] ? "T" : "F",
                                            truthValues[1] ? "T" : "F",
                                            truthValues[2] ? "T" : "F",
                                            truthValues[3] ? "T" : "F");

    return formattedValues;
}

       private string Equations()
{
     Random rnd = new Random(Guid.NewGuid().GetHashCode());

    // Propositions without negation
    string[] propositionsWithoutNegation = { "p", "q", "r", "s" };

    // Propositions with negation
    string[] propositionsWithNegation = { "-p", "-q", "-r", "-s" };

    // Operators
    string[] operators = { "v", "^", "->", "<->" };

    string prop1, prop2;

    // Check if the score is less than 10
    if (score < 20)
    {
        prop1 = propositionsWithoutNegation[rnd.Next(propositionsWithoutNegation.Length)];

        // Ensure prop2 is different from prop1
        do
        {
            prop2 = propositionsWithoutNegation[rnd.Next(propositionsWithoutNegation.Length)];
        } while (prop2 == prop1);
    }
    else
    {
        // Randomly select whether to include negation or not
        bool includeNegation = rnd.Next(2) == 0;

        if (includeNegation)
        {
            prop1 = propositionsWithNegation[rnd.Next(propositionsWithNegation.Length)];

            // Ensure prop2 is different from prop1
            do
            {
                prop2 = propositionsWithNegation[rnd.Next(propositionsWithNegation.Length)];
            } while (prop2 == prop1);
        }
        else
        {
            prop1 = propositionsWithoutNegation[rnd.Next(propositionsWithoutNegation.Length)];

            // Ensure prop2 is different from prop1
            do
            {
                prop2 = propositionsWithoutNegation[rnd.Next(propositionsWithoutNegation.Length)];
            } while (prop2 == prop1);
        }
    }

    // Randomly select an operator
    string op = operators[rnd.Next(operators.Length)];

    // Combine the propositions and operator to form the problem
    return prop1 + op + prop2;
}
       private string Equations2()
{
    Random rnd = new Random(Guid.NewGuid().GetHashCode());

    // Propositions with negation
    string[] propositionsWithNegation = { "-p", "-q", "-r", "-s" };

    // Operators


    // Select propositions with negation
    string prop1 = propositionsWithNegation[rnd.Next(propositionsWithNegation.Length)];


    // Combine the propositions and operator to form the problem
    return prop1;
}
       private string Equations3()
{
    Random rnd = new Random(Guid.NewGuid().GetHashCode());
    // Propositions without negation
    string[] propositionsWithoutNegation = { "p", "q", "r", "s" };

    // Propositions with negation
    string[] propositionsWithNegation = { "-p", "-q", "-r", "-s" };

    // Operators
    string[] operators = { "v", "^", "->", "<->" };

    string prop1, prop2;

    if (score < 25)
    {
        prop1 = propositionsWithoutNegation[rnd.Next(propositionsWithoutNegation.Length)];

        // Ensure prop2 is different from prop1
        do
        {
            prop2 = propositionsWithoutNegation[rnd.Next(propositionsWithoutNegation.Length)];
        } while (prop2 == prop1);
    }
    else
    {
        // Randomly select whether to include negation or not
        bool includeNegation = rnd.Next(2) == 0;

        if (includeNegation)
        {
            prop1 = propositionsWithNegation[rnd.Next(propositionsWithNegation.Length)];

            // Ensure prop2 is different from prop1
            do
            {
                prop2 = propositionsWithNegation[rnd.Next(propositionsWithNegation.Length)];
            } while (prop2 == prop1);
        }
        else
        {
            prop1 = propositionsWithoutNegation[rnd.Next(propositionsWithoutNegation.Length)];

            // Ensure prop2 is different from prop1
            do
            {
                prop2 = propositionsWithoutNegation[rnd.Next(propositionsWithoutNegation.Length)];
            } while (prop2 == prop1);
        }
    }

    // Randomly select an operator
    string op = operators[rnd.Next(operators.Length)];

    // Combine the propositions and operator to form the problem
    return prop1 + op + prop2;
}

        private string DetermineAnswer()
{
    string truthValues = label3.Text;

    string[] splitValues = truthValues.Split(' ');

    bool[] boolValues = new bool[8]; // Increase size to accommodate negation
    for (int i = 0; i < splitValues.Length; i++)
    {
        boolValues[i] = splitValues[i].EndsWith("T");
    }

    string equation = label1.Text;

    string[] equationParts = equation.Split(new[] { "v", "^", "->", "<->" }, StringSplitOptions.RemoveEmptyEntries);
    string prop1 = equationParts[0].Trim();
    string prop2 = equationParts[1].Trim();
    string op = equation.Replace(prop1, "").Replace(prop2, "").Trim(); // Get the operator

    // Evaluate the equation based on truth values
    bool answer;
            switch (op)
            {
            		case "-":
            answer = prop1.StartsWith("-") ? !boolValues[Array.IndexOf(splitValues, prop1.Substring(1))] : !boolValues[Array.IndexOf(splitValues, prop2.Substring(1))];
            break;
                case "v": // OR operator
                    answer = (prop1 == "p" && prop2 == "q") || (prop1 == "q" && prop2 == "p")
                        ? boolValues[0] || boolValues[1]
                        : (prop1 == "p" && prop2 == "r") || (prop1 == "r" && prop2 == "p")
                            ? boolValues[0] || boolValues[2]
                            : (prop1 == "p" && prop2 == "s") || (prop1 == "s" && prop2 == "p")
                                ? boolValues[0] || boolValues[3]
                                : (prop1 == "q" && prop2 == "r") || (prop1 == "r" && prop2 == "q")
                                    ? boolValues[1] || boolValues[2]
                                    : (prop1 == "q" && prop2 == "s") || (prop1 == "s" && prop2 == "q")
                                        ? boolValues[1] || boolValues[3]
                                        : (prop1 == "r" && prop2 == "s") || (prop1 == "s" && prop2 == "r")
                                            ? boolValues[2] || boolValues[3]
                    	 : (prop1 == "p" && prop2 == "p")
                         ? boolValues[0]
                         : (prop1 == "r" && prop2 == "r")
                           ? boolValues[2]
                           : (prop1 == "s" && prop2 == "s")
                             ? boolValues[3]
                             : (prop1 == "q" && prop2 == "q")
                               ? boolValues[1]
                                            : false;
                    break;
                case "^": // AND operator
                    answer = (prop1 == "p" && prop2 == "q") || (prop1 == "q" && prop2 == "p")
                        ? boolValues[0] && boolValues[1]
                        : (prop1 == "p" && prop2 == "r") || (prop1 == "r" && prop2 == "p")
                            ? boolValues[0] && boolValues[2]
                            : (prop1 == "p" && prop2 == "s") || (prop1 == "s" && prop2 == "p")
                                ? boolValues[0] && boolValues[3]
                                : (prop1 == "q" && prop2 == "r") || (prop1 == "r" && prop2 == "q")
                                    ? boolValues[1] && boolValues[2]
                                    : (prop1 == "q" && prop2 == "s") || (prop1 == "s" && prop2 == "q")
                                        ? boolValues[1] && boolValues[3]
                                        : (prop1 == "r" && prop2 == "s") || (prop1 == "s" && prop2 == "r")
                                            ? boolValues[2] && boolValues[3]
                    	 : (prop1 == "p" && prop2 == "p")
                         ? boolValues[0]
                         : (prop1 == "r" && prop2 == "r")
                           ? boolValues[2]
                           : (prop1 == "s" && prop2 == "s")
                             ? boolValues[3]
                             : (prop1 == "q" && prop2 == "q")
                               ? boolValues[1]
                                            : false;
                    break;
                   case "->": // IMPLIES operator
    answer = (prop1 == "p" && prop2 == "q")
        ? !boolValues[0] || boolValues[1]  // p->q is true unless p is true and q is false
        : (prop1 == "p" && prop2 == "r")
            ? !boolValues[0] || boolValues[2] // p->r is true unless p is true and r is false
            : (prop1 == "p" && prop2 == "s")
                ? !boolValues[0] || boolValues[3] // p->s is true unless p is true and s is false
                : (prop1 == "q" && prop2 == "r")
                    ? !boolValues[1] || boolValues[2] // q->r is true unless q is true and r is false
                    : (prop1 == "q" && prop2 == "s")
                        ? !boolValues[1] || boolValues[3] // q->s is true unless q is true and s is false
                        : (prop1 == "r" && prop2 == "s")
                            ? !boolValues[2] || boolValues[3] // r->s is true unless r is true and s is false
                            : true; // Default case, return true for other scenarios
    break;

          case "<->": // BICONDITIONAL operator
    answer = (prop1 == "p" && prop2 == "q") || (prop1 == "q" && prop2 == "p")
        ? boolValues[0] == boolValues[1] // Check if p and q have the same truth value
        : (prop1 == "p" && prop2 == "r") || (prop1 == "r" && prop2 == "p")
            ? boolValues[0] == boolValues[2] // Check if p and r have the same truth value
            : (prop1 == "p" && prop2 == "s") || (prop1 == "s" && prop2 == "p")
                ? boolValues[0] == boolValues[3] // Check if p and s have the same truth value
                : (prop1 == "q" && prop2 == "r") || (prop1 == "r" && prop2 == "q")
                    ? boolValues[1] == boolValues[2] // Check if q and r have the same truth value
                    : (prop1 == "q" && prop2 == "s") || (prop1 == "s" && prop2 == "q")
                        ? boolValues[1] == boolValues[3] // Check if q and s have the same truth value
                        : (prop1 == "r" && prop2 == "s") || (prop1 == "s" && prop2 == "r")
                            ? boolValues[2] == boolValues[3] // Check if r and s have the same truth value
                            : (prop1 == "p" && prop2 == "p") // Check if p and p are the same proposition
                                ? true // Return true
                                : (prop1 == "q" && prop2 == "q") // Check if q and q are the same proposition
                                    ? true // Return true
                                    : (prop1 == "r" && prop2 == "r") // Check if r and r are the same proposition
                                        ? true // Return true
                                        : (prop1 == "s" && prop2 == "s") // Check if s and s are the same proposition
                                            ? true // Return true
                                            : false; // Default case, return false
break;
                default:
                    throw new ArgumentException("Invalid operator");
            }

            // Convert the boolean answer to string format ("T" or "F")
            string answerString = answer ? "T" : "F";

            // Return the answer
            return answerString;
        }
       private string DetermineAnswer8()
{
    string truthValues = label3.Text; // Example: "p=T q=F r=T s=F"
    string[] splitValues = truthValues.Split(' ');

    bool[] boolValues = new bool[4];
    boolValues[0] = splitValues[0].EndsWith("T");
    boolValues[1] = splitValues[1].EndsWith("T");
    boolValues[2] = splitValues[2].EndsWith("T");
    boolValues[3] = splitValues[3].EndsWith("T");

    string equation = label8.Text; // Example: "-p"

    bool prop1Value;
    switch (equation)
    {
        case "-p":
            prop1Value = !boolValues[0];
            break;
        case "-q":
            prop1Value = !boolValues[1];
            break;
        case "-r":
            prop1Value = !boolValues[2];
            break;
        case "-s":
            prop1Value = !boolValues[3];
            break;
        case "p":
            prop1Value = boolValues[0];
            break;
        case "q":
            prop1Value = boolValues[1];
            break;
        case "r":
            prop1Value = boolValues[2];
            break;
        case "s":
            prop1Value = boolValues[3];
            break;
        default:
            throw new ArgumentException("Invalid proposition");
    }

    // Convert the boolean answer to string format ("T" or "F")
    string answerString = prop1Value ? "T" : "F";

    // Return the answer
    return answerString;
}
        private string DetermineAnswer9()
{
    string truthValues = label3.Text;

    string[] splitValues = truthValues.Split(' ');

    bool[] boolValues = new bool[8]; // Increase size to accommodate negation
    for (int i = 0; i < splitValues.Length; i++)
    {
        boolValues[i] = splitValues[i].EndsWith("T");
    }

    string equation = label9.Text;

    string[] equationParts = equation.Split(new[] { "v", "^", "->", "<->" }, StringSplitOptions.RemoveEmptyEntries);
    string prop1 = equationParts[0].Trim();
    string prop2 = equationParts[1].Trim();
    string op = equation.Replace(prop1, "").Replace(prop2, "").Trim(); // Get the operator

    // Evaluate the equation based on truth values
    bool answer;
            switch (op)
            {
            		case "-":
            answer = prop1.StartsWith("-") ? !boolValues[Array.IndexOf(splitValues, prop1.Substring(1))] : !boolValues[Array.IndexOf(splitValues, prop2.Substring(1))];
            break;
                case "v": // OR operator
                    answer = (prop1 == "p" && prop2 == "q") || (prop1 == "q" && prop2 == "p")
                        ? boolValues[0] || boolValues[1]
                        : (prop1 == "p" && prop2 == "r") || (prop1 == "r" && prop2 == "p")
                            ? boolValues[0] || boolValues[2]
                            : (prop1 == "p" && prop2 == "s") || (prop1 == "s" && prop2 == "p")
                                ? boolValues[0] || boolValues[3]
                                : (prop1 == "q" && prop2 == "r") || (prop1 == "r" && prop2 == "q")
                                    ? boolValues[1] || boolValues[2]
                                    : (prop1 == "q" && prop2 == "s") || (prop1 == "s" && prop2 == "q")
                                        ? boolValues[1] || boolValues[3]
                                        : (prop1 == "r" && prop2 == "s") || (prop1 == "s" && prop2 == "r")
                                            ? boolValues[2] || boolValues[3]
                    	 : (prop1 == "p" && prop2 == "p")
                         ? boolValues[0]
                         : (prop1 == "r" && prop2 == "r")
                           ? boolValues[2]
                           : (prop1 == "s" && prop2 == "s")
                             ? boolValues[3]
                             : (prop1 == "q" && prop2 == "q")
                               ? boolValues[1]
                                            : false;
                    break;
                case "^": // AND operator
                    answer = (prop1 == "p" && prop2 == "q") || (prop1 == "q" && prop2 == "p")
                        ? boolValues[0] && boolValues[1]
                        : (prop1 == "p" && prop2 == "r") || (prop1 == "r" && prop2 == "p")
                            ? boolValues[0] && boolValues[2]
                            : (prop1 == "p" && prop2 == "s") || (prop1 == "s" && prop2 == "p")
                                ? boolValues[0] && boolValues[3]
                                : (prop1 == "q" && prop2 == "r") || (prop1 == "r" && prop2 == "q")
                                    ? boolValues[1] && boolValues[2]
                                    : (prop1 == "q" && prop2 == "s") || (prop1 == "s" && prop2 == "q")
                                        ? boolValues[1] && boolValues[3]
                                        : (prop1 == "r" && prop2 == "s") || (prop1 == "s" && prop2 == "r")
                                            ? boolValues[2] && boolValues[3]
                    	 : (prop1 == "p" && prop2 == "p")
                         ? boolValues[0]
                         : (prop1 == "r" && prop2 == "r")
                           ? boolValues[2]
                           : (prop1 == "s" && prop2 == "s")
                             ? boolValues[3]
                             : (prop1 == "q" && prop2 == "q")
                               ? boolValues[1]
                                            : false;
                    break;
                   case "->": // IMPLIES operator
    answer = (prop1 == "p" && prop2 == "q")
        ? !boolValues[0] || boolValues[1]  // p->q is true unless p is true and q is false
        : (prop1 == "p" && prop2 == "r")
            ? !boolValues[0] || boolValues[2] // p->r is true unless p is true and r is false
            : (prop1 == "p" && prop2 == "s")
                ? !boolValues[0] || boolValues[3] // p->s is true unless p is true and s is false
                : (prop1 == "q" && prop2 == "r")
                    ? !boolValues[1] || boolValues[2] // q->r is true unless q is true and r is false
                    : (prop1 == "q" && prop2 == "s")
                        ? !boolValues[1] || boolValues[3] // q->s is true unless q is true and s is false
                        : (prop1 == "r" && prop2 == "s")
                            ? !boolValues[2] || boolValues[3] // r->s is true unless r is true and s is false
                            : true; // Default case, return true for other scenarios
    break;

          case "<->": // BICONDITIONAL operator
    answer = (prop1 == "p" && prop2 == "q") || (prop1 == "q" && prop2 == "p")
        ? boolValues[0] == boolValues[1] // Check if p and q have the same truth value
        : (prop1 == "p" && prop2 == "r") || (prop1 == "r" && prop2 == "p")
            ? boolValues[0] == boolValues[2] // Check if p and r have the same truth value
            : (prop1 == "p" && prop2 == "s") || (prop1 == "s" && prop2 == "p")
                ? boolValues[0] == boolValues[3] // Check if p and s have the same truth value
                : (prop1 == "q" && prop2 == "r") || (prop1 == "r" && prop2 == "q")
                    ? boolValues[1] == boolValues[2] // Check if q and r have the same truth value
                    : (prop1 == "q" && prop2 == "s") || (prop1 == "s" && prop2 == "q")
                        ? boolValues[1] == boolValues[3] // Check if q and s have the same truth value
                        : (prop1 == "r" && prop2 == "s") || (prop1 == "s" && prop2 == "r")
                            ? boolValues[2] == boolValues[3] // Check if r and s have the same truth value
                            : (prop1 == "p" && prop2 == "p") // Check if p and p are the same proposition
                                ? true // Return true
                                : (prop1 == "q" && prop2 == "q") // Check if q and q are the same proposition
                                    ? true // Return true
                                    : (prop1 == "r" && prop2 == "r") // Check if r and r are the same proposition
                                        ? true // Return true
                                        : (prop1 == "s" && prop2 == "s") // Check if s and s are the same proposition
                                            ? true // Return true
                                            : false; // Default case, return false
break;
                default:
                    throw new ArgumentException("Invalid operator");
            }

            // Convert the boolean answer to string format ("T" or "F")
            string answerString = answer ? "T" : "F";

            // Return the answer
            return answerString;
        }


      private void Button1Click(object sender, EventArgs e)
{
      	timer4.Start();
      	button1.Enabled = false;
      	button2.Enabled = false;
    HandleButtonClick("T");
    if (timer4.Interval == 100){
    	button1.Enabled = true;
      	button2.Enabled = true;
    }
      	
}

private void Button2Click(object sender, EventArgs e)
{
	timer4.Start();
      	button1.Enabled = false;
      	button2.Enabled = false;
    HandleButtonClick("F");
    if (timer4.Interval == 100){
    	button1.Enabled = true;
      	button2.Enabled = true;
    }
}

private void HandleButtonClick(string expectedAnswer)
{
    Label closestLabel = GetClosestLabel();

    if (closestLabel == null)
    {
        // Handle case when no label is found
        return;
    }

    // Highlight the closest label

    string answer;
    switch (closestLabel.Name)
    {
        case "label1":
            answer = DetermineAnswer();
            if (answer == expectedAnswer)
            {
                label1.Text = "✔";
                score++;
                score++;
                label5.Text = "Score: " + score;

              
                UpdateProgressBar();
            }
            else
            {
                label1.Text = "❌";
                TakeDamage();
                progressCount = 0;
            progressBar1.Value = 0;
            }

            Application.DoEvents();
            Thread.Sleep(1000);
            closestLabel.Text = Equations();
            ResetLabelLocation(closestLabel);
            // Reset the background color of the closest label
            ResetLabelColor(closestLabel);
            break;
        case "label8":
            answer = DetermineAnswer8();
            if (answer == expectedAnswer)
            {
                label8.Text = "✔";
                score++;
                label5.Text = "Score: " + score;

          
                UpdateProgressBar();
            }
            else
            {
                label8.Text = "❌";
                TakeDamage();
                progressCount = 0;
            progressBar1.Value = 0;
            }

            Application.DoEvents();
            Thread.Sleep(1000);
            label8.Text = Equations2();
            ResetLabelLocation(closestLabel);
            // Reset the background color of the closest label
            ResetLabelColor(closestLabel);
            break;
        case "label9":
            answer = DetermineAnswer9();
            if (answer == expectedAnswer)
            {
                label9.Text = "✔";
                score++;
                score++;
                label5.Text = "Score: " + score;
               
                UpdateProgressBar();
            }
            else
            {
                label9.Text = "❌";
                TakeDamage();
                progressCount = 0;
            progressBar1.Value = 0;
            }

            Application.DoEvents();
            Thread.Sleep(1000);
            label9.Text = Equations3();
            ResetLabelLocation(closestLabel);
            // Reset the background color of the closest label
            ResetLabelColor(closestLabel);
            break;
        default:
            // Handle case for other labels
            return;
    }
}

private void HighlightClosestLabel(Label label)
{
    // Reset background color of all labels
    label1.BackColor = SystemColors.Control;
    label8.BackColor = SystemColors.Control;
    label9.BackColor = SystemColors.Control;

    // Highlight the closest label
    if (label != null)
    {
        label.BackColor = Color.LightBlue;
    }
}



private void ResetLabelColor(Label label)
{
    // Reset the background color of the label to its default color
    label.BackColor = SystemColors.Control;
}

private void ResetLabelLocation(Label label)
{
	label.Top = 0;
    label.Left = new Random().Next(0, this.Width - label.Width); // Adjust to the form's width
}

private void Equation_Movement3()
{
    if (label9.Top >= 460)
    {
        ResetLabelLocation(label9);
    }

    if (label9.Top >= 450)
    {
        label8.Text = "❌";
        TakeDamage();
        Application.DoEvents();
        Thread.Sleep(1000);
        label9.Text = Equations3();
    }

    label9.Top += 10;
}
private void Equation_Movement2()
{
    if (label8.Top >= 460)
    {
        ResetLabelLocation(label8);
    }

    if (label8.Top >= 450)
    {
        label8.Text = "❌";
        TakeDamage();
        Application.DoEvents();
        Thread.Sleep(1000);
        label8.Text = Equations2();
    }

    label8.Top += 15;
}

private void Equation_Movement()
{
    if (label1.Top >= 460)
    {
        ResetLabelLocation(label1);
    }

    if (label1.Top >= 450)
    {
        label1.Text = "❌";
        TakeDamage();
        Application.DoEvents();
        Thread.Sleep(1000);
        label1.Text = Equations();
    }

    label1.Top += 15;
}

        private void MainFormKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) // Check for the 'A' key press
            {
                button1.PerformClick(); // Simulate button click
            }
            else if (e.KeyCode == Keys.D) // Check for the 'D' key press
            {
                button2.PerformClick(); // Simulate button click
            }
        }
       private void TakeDamage()
{
 
        health--; // Decrement health by one

        // Update the visibility of the heart icons based on the current health
        switch (health)
        {
        	case 3:
        		pictureBox4.Visible = true; // show three hearts
                pictureBox3.Visible = false; // Hide two hearts
                pictureBox2.Visible = false; // Hide one heart
                pictureBox1.Visible = false;  // Hide no heart
                break;
            case 2:
                pictureBox4.Visible = false; // Hide three hearts
                pictureBox3.Visible = true;  // Show two hearts
                pictureBox2.Visible = false; // Hide one heart
                pictureBox1.Visible = false; // Hide no heart
                break;
            case 1:
                pictureBox4.Visible = false; // Hide three hearts
                pictureBox3.Visible = false; // Hide two hearts
                pictureBox2.Visible = true;  // Show one heart
                pictureBox1.Visible = false; // Hide no heart
                break;
            case 0:
                button1.Enabled = false;button2.Enabled = false;
                pictureBox4.Visible = false; // Hide three hearts
                pictureBox3.Visible = false; // Hide two hearts
                pictureBox2.Visible = false; // Hide one heart
                pictureBox1.Visible = true;  // Show no heart
                GameOver();
                break;
        
    }
}
        private void Heal()
{
 
        health++; // Decrement health by one

        // Update the visibility of the heart icons based on the current health
        switch (health)
        {
        	case 3:
        		pictureBox4.Visible = true; // show three hearts
                pictureBox3.Visible = false; // Hide two hearts
                pictureBox2.Visible = false; // Hide one heart
                pictureBox1.Visible = false;  // Hide no heart
                break;
            case 2:
                pictureBox4.Visible = false; // Hide three hearts
                pictureBox3.Visible = true;  // Show two hearts
                pictureBox2.Visible = false; // Hide one heart
                pictureBox1.Visible = false; // Hide no heart
                break;
            case 1:
                pictureBox4.Visible = false; // Hide three hearts
                pictureBox3.Visible = false; // Hide two hearts
                pictureBox2.Visible = true;  // Show one heart
                pictureBox1.Visible = false; // Hide no heart
                break;
            case 0:
                pictureBox4.Visible = false; // Hide three hearts
                pictureBox3.Visible = false; // Hide two hearts
                pictureBox2.Visible = false; // Hide one heart
                pictureBox1.Visible = true;  // Show no heart
                GameOver();
                break;
        
    }
}
public void GameOver()
{
    button1.Enabled = false;
    button2.Enabled = false;
    label4.Text = "HighScore: " + highScore;
    label6.Text = label5.Text;
    timer1.Enabled = false;
    timer2.Enabled = false;
    timer3.Enabled = false;
    groupBox1.Visible = true;

    if (score > highScore)
    {
        highScore = score;
        SaveHighScore(); // Save high score if it's beaten
    }
}

private void RestartGame()
{
    label7.Text = "HighScore: " + highScore;
    pictureBox4.Visible = true;
    health = 3;
    score = 0;
    label5.Text = "Score: " + score;
    label3.Text = TruthValues();

    // Set fixed starting positions for label1, label8, and label9
    const int label1Top = 0;
    const int label8Top = 0;
    const int label9Top = 80;

    label1.Top = label1Top;
    label8.Top = label8Top;
    label9.Top = label9Top;

    label1.Text = Equations();
    label8.Text = Equations2();
    label9.Text = Equations3();

    groupBox1.Visible = false;

    // Reset visibility and timer states
    label9.Visible = true;
    label8.Visible = true;
    timer1.Enabled = false;
    timer2.Enabled = false;
    timer3.Enabled = false;

    // Start the timers after resetting
    timer1.Enabled = true;
    timer2.Enabled = true;
    timer3.Enabled = true;
}
	
		void Button3Click(object sender, EventArgs e)
		{
			RestartGame();
		}
private void Timer1Tick(object sender, EventArgs e)
{
	UpdateProgressBar();
    Equation_Movement();
    Label closestLabel = GetClosestLabel();
    HighlightClosestLabel(closestLabel);
}

void Timer2Tick(object sender, EventArgs e)
{
	
	if (score >= 8){
		Equation_Movement2();
	     label8.Visible = true;
	}
}
void Timer3Tick(object sender, EventArgs e)
{
	if (score >=20){
		Equation_Movement3();
	     label9.Visible = true;
	}

    
}
		
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			panel1.Visible=true;
			button4.Visible=true;
			button5.Visible=true;
			label10.Visible=true;
			button5.Click += Button5Click;
		}
		
		
		void Button4Click(object sender, EventArgs e)
		{
				Application.Exit();
		}
		
		
		void Button5Click(object sender, EventArgs e)
		{
			panel1.Visible=false;
			button4.Visible=false;
			button5.Visible=false;
			label10.Visible=false;
		}
		private void UpdateProgressBar()
{
    if ((label1.Text == "✔" || label8.Text == "✔" || label9.Text == "✔") && progressCount < progressBar1.Maximum)
    {
        progressCount++;
        progressBar1.Value = progressCount;
        progressBar1.Maximum = 7;

        if (progressCount >= progressBar1.Maximum)
        {
            if (health == 3)
            {
                // Increment score if health is 3 and progress bar is full
                score++;
                label5.Text = "Score: " + score;
            }
            else
            {
                // Decrementing health while progress bar is full
                Heal(); // Automatically heal
                progressBar1.Value = 0; // Reset progress bar to zero
                progressCount = 0;
            }
        }
    }
    else if (health < 3 && progressCount >= progressBar1.Maximum)
    {
        // Decrementing health while progress bar is full and health < 3
        Heal(); // Automatically heal
        progressBar1.Value = 0; // Reset progress bar to zero
        progressCount = 0;
    }
}
		
		
		
	
		
	
		
	
		
		void RadioButton1CheckedChanged(object sender, EventArgs e)
{
    if (radioButton1.Checked)
    {
        if (score % 10 == 0) // Check if the score has increased by 10 points
                {
                    label3.Text = TruthValues();
                    score++;
                }
    }
}
		
		void Button6Click(object sender, EventArgs e)
		{
			pictureBox1.Visible = false;
			pictureBox2.Visible = false;
			pictureBox3.Visible = false;
			pictureBox4.Visible = false;
			panel2.Visible = true;
			timer1.Enabled=false;
			label8.Visible=false;
            label9.Visible=false;
             label1.Visible=false;
             button1.Visible=false;
             button2.Visible=false;
             progressBar1.Visible=false;
             label3.Visible=false;
               label7.Visible = false;
           label5.Visible = false;
           button8.Visible = true;
           radioButton1.Visible= true;
               groupBox1.Visible = false;
		}
		void Panel2Paint(object sender, PaintEventArgs e)
		{
	
		//}
		//void PictureBox5Click(object sender, EventArgs e)
		//{
	
		//}
		//void BtnRulesClick(object sender, EventArgs e)
		//{
		//	Rules rule = new Rules();
		//	rule.Show();
		//}
		//void Label9Click(object sender, EventArgs e)
		//{
	
		//}
		void Panel3Paint(object sender, PaintEventArgs e)
		{
	
		}
		void Label3Click(object sender, EventArgs e)
		{
	
		}
		void Label8Click(object sender, EventArgs e)
		{
	
		}
		void Panel4Paint(object sender, PaintEventArgs e)
		{
	
		}
		void PictureBox4Click(object sender, EventArgs e)
		{
	
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked){
				SoundPlayer song = new SoundPlayer("Song.wav");
            song.Play();
			}
			else{
            song.Stop();
			}
	
		}
		void BtnExitClick(object sender, EventArgs e)
		{
			this.Close();
		}

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
    }

    