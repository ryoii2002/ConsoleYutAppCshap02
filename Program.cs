using System.Runtime;

namespace ConsoleYutAppCshap02
{
    internal class Program
    {
        static int totalSum = 0; //던진후 전진할수 있는 칸수 합계
        static public int userShortcut = 0; // 1 : "1번째지름길이용중", 2 : "2번째지름길이용중".
        static public int userShortcutCount = 0; //지름길 이용횟수
        static int userTotalPoint = 20;  //사용자의 총 가야할 칸수(지름길이용시 줄어듬)
        static int userMovePoint = 0; //사용자의 총 이동한 칸 수.
        static public int comShortcut = 0; // 1 : "1번째지름길이용중", 2 : "2번째지름길이용중".
        static public int comShortcutCount = 0; //지름길 이용횟수
        static int comTotalPoint = 20;   //컴퓨터의 총 가야할 칸수(지름길이용시 줄어듬)
        static int comMovePoint = 0; //컴퓨터의 총 이동한 칸 수.

        //말판용 변수들. 충돌시 상대말 잡을때 사용.
        static int userBasicBoard; // 사용자 말판 고유번호. 
        static int userShortcutBoardFirst; // 1번 지름길 고유번호. 
        static int userShortcutBoardSecond; // 2번 지름길 고유번호. 

        static int comBasicBoard; // 컴퓨터 말판보드 고유번호. 
        static int comShortcutBoardFirst; // 1번 지름길 고유번호. 
        static int comShortcutBoardSecond; // 2번 지름길 고유번호. 
        static void Main(string[] args)
        {
            Console.WriteLine("윷놀이 시작합니다.");

            bool gameRunnig = true;
            string nowPlayerName = "사용자"; //사용자, 컴퓨터.

            int userEndPoint;  //사용자가 앞으로 가야할 칸 수.
            
            int comEndPoint;   //컴퓨터가 앞으로 가야할 칸 수.

            while (gameRunnig)
            {
                totalSum = 0;
                if (nowPlayerName == "사용자")
                {
                    Console.WriteLine($"===========================================");
                    Console.WriteLine($"당신의 차례입니다!");
                    Console.WriteLine($"윷을 던지려면 1을 입력하세요. ");

                    while (true)
                    {

                        roll();                    //윷 던지기

                        userMovePoint += totalSum; //지금까지 이동한 칸수의 총합 + 방금 진행한 칸수

                        ////////////말의 위치 저장
                        if (userShortcut == 0)
                        {
                            userBasicBoard += totalSum;
                            userShortcutBoardFirst = 0;
                            userShortcutBoardSecond = 0;

                        }
                        else if (userShortcut == 1 && userShortcutCount == 1) //1번 지름길 이용중이면서 지름길 이용횟수 1회,
                        {
                            userBasicBoard = 0;
                            userShortcutBoardFirst += totalSum;
                            userShortcutBoardSecond = 0;
                            if (userShortcutBoardFirst > 5) // 1번 지름길의 끝을 넘어 가면 Basic코스도 넣어줌.
                            {
                                userBasicBoard = userShortcutBoardFirst - 5 + 14;
                            }

                        }
                        else if (userShortcut == 1 && userShortcutCount == 2) //1번 지름길 이용중이면서 두번째 지름길도 진입함.
                        {
                            if (userShortcutBoardFirst == 3)
                            {
                                userShortcutBoardSecond = 3;
                            }
                            userBasicBoard = 0;
                            userShortcutBoardFirst = 0;
                            userShortcutBoardSecond += totalSum;
                        }
                        else if (userShortcut == 2) // 2번 지름길 이용중
                        {
                            userBasicBoard = 0;
                            userShortcutBoardFirst = 0;
                            userShortcutBoardSecond += totalSum;
                        }
                        else { Console.WriteLine($"오류당!!"); }

                        /////////////////////
                        if (!is_catch_available(nowPlayerName))//상대방 말 잡으면 계속 던짐.
                        {
                            break;
                        }

                    }
                    
                    if (is_shortcut_available(userMovePoint, nowPlayerName))
                    {
                        Console.WriteLine($"지름길로 가자.%%%%%%%%%%%%{userTotalPoint}%{userMovePoint}%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                        using_shortcut(userMovePoint, nowPlayerName);
                    }
                    userEndPoint = userTotalPoint - userMovePoint;  //가야할 칸 총수 - 지금까지 진행한 칸수

                    Console.WriteLine($"이동할 수 있는 칸수는  {totalSum}칸 입니다. ");
                    Console.WriteLine($"당신 말이 이동한 칸수는 {userMovePoint} 입니다. ");
                    if (userMovePoint > userTotalPoint)
                    {
                        Console.WriteLine($"완주했습니다!");
                        Console.WriteLine($"===========================================");
                        Console.WriteLine($"");
                        Console.WriteLine($"사용자 승리!");
                        Console.WriteLine($"게임 종료");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"완주까지 {userEndPoint}칸 남았습니다. ");
                        Console.WriteLine($"현재말의위치  basic: {userBasicBoard} firstShotcut : {userShortcutBoardFirst} secondShotcut : {userShortcutBoardSecond}");
                    }
                    
                    nowPlayerName = "컴퓨터";

                }
                
                else if (nowPlayerName == "컴퓨터")
                {
                    Console.WriteLine($"===========================================");
                    Console.WriteLine($"컴퓨터의 차례입니다!");
                    Console.WriteLine($"윷을 던지게 하려면 1을 입력하세요. ");
                    while(true)
                    {
                        roll();                    //윷 던지기
                        comMovePoint += totalSum; //지금까지 이동한 칸수의 총합 + 방금 진행한 칸수
                                                  ////////////말의 위치 저장
                        if (comShortcut == 0)
                        {
                            comBasicBoard += totalSum;
                            comShortcutBoardFirst = 0;
                            comShortcutBoardSecond = 0;

                        }
                        else if (comShortcut == 1 && comShortcutCount == 1) //1번 지름길 이용중이면서 지름길 이용횟수 1회,
                        {
                            comBasicBoard = 0;
                            comShortcutBoardFirst += totalSum;
                            comShortcutBoardSecond = 0;
                            if (comShortcutBoardFirst > 5) // 1번 지름길의 끝을 넘어 가면 Basic코스도 넣어줌.
                            {
                                comBasicBoard = comShortcutBoardFirst - 5 + 14;
                            }

                        }
                        else if (comShortcut == 1 && comShortcutCount == 2) //1번 지름길 이용중이면서 두번째 지름길도 진입함.
                        {
                            if (comShortcutBoardFirst == 3)
                            {
                                comShortcutBoardSecond = 3;
                            }
                            comBasicBoard = 0;
                            comShortcutBoardFirst = 0;
                            comShortcutBoardSecond += totalSum;
                        }
                        else if (comShortcut == 2) // 2번 지름길 이용중
                        {
                            comBasicBoard = 0;
                            comShortcutBoardFirst = 0;
                            comShortcutBoardSecond += totalSum;
                        }
                        else { Console.WriteLine($"오류당!!"); }

                        /////////////////////
                        if (!is_catch_available(nowPlayerName))//상대방 말 잡으면 계속 던짐.
                        {
                            break;
                        }
                    }

                    if (is_shortcut_available(comMovePoint, nowPlayerName))
                    {
                        Console.WriteLine($"지름길로 가자.???????????????{comTotalPoint}%{comMovePoint}????????????????????????????????????");
                        using_shortcut(comMovePoint, nowPlayerName);
                    }
                    comEndPoint = comTotalPoint - comMovePoint;  //가야할 칸 총수 - 지금까지 진행한 칸수
                    Console.WriteLine($"이동할 수 있는 칸수는  {totalSum}칸 입니다. ");
                    Console.WriteLine($"컴퓨터 말이 이동한 칸수는 {comMovePoint} 입니다. ");
                    if (comMovePoint > comTotalPoint)
                    {
                        Console.WriteLine($"완주했습니다!");
                        Console.WriteLine($"===========================================");
                        Console.WriteLine($"");
                        Console.WriteLine($"컴퓨터 승리!");
                        Console.WriteLine($"게임 종료");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"완주까지 {comEndPoint}칸 남았습니다. ");
                        Console.WriteLine($"현재말의위치  basic: {comBasicBoard} first : {comShortcutBoardFirst} second : {comShortcutBoardSecond}");
                    }
                    nowPlayerName = "사용자";
                }
                
            }
        }

        //1)윷 던지는 함수.
        static int roll()
        {
            int yut1; //1번 
            int yut2; //2번 
            int yut3; //3번 
            int yut4; //4번 

            Random random = new Random();
            yut1 = random.Next(0, 2);
            yut2 = random.Next(0, 2);
            yut3 = random.Next(0, 2);
            yut4 = random.Next(0, 2);

            totalSum = yut1 + yut2 + yut3 + yut4;

            if (totalSum == 0) {
                Console.WriteLine($"모가 나왔습니다. 1을 입력하면 한번 더 던집니다! ");
                totalSum = 5 + roll();
            } 
            else if (totalSum == 1 && yut1 == 1) // 빽도라면
            {
                Console.WriteLine($"빽도가 나왔습니다.");
                totalSum = -1;
            } 
            else if (totalSum == 4)
            {
                Console.WriteLine($"윷이 나왔습니다. 1을 입력하면 한번 더 던집니다! ");
                totalSum = 4 + roll();
            } 
            else if (totalSum == 1)
            {
                Console.WriteLine($"도가 나왔습니다.");
            }
            else if (totalSum == 2)
            {
                Console.WriteLine($"개가 나왔습니다.");
            }
            else if (totalSum == 3)
            {
                Console.WriteLine($"걸이 나왔습니다.");
            }
            Console.WriteLine("totalSum = " + totalSum + " / " + yut1 + " " + yut2 + " " + yut3 + " " + yut4);
            return totalSum;
        }

        //2)현재위치에서 지름길 사용이 가능한지 판별하는 함수.
        public static bool is_shortcut_available(int MovePoint, string nowPlayerName)
        {
            if ((MovePoint == 5 || MovePoint == 10) && userShortcut == 0 && nowPlayerName == "사용자")
            {
                return true;

            }else if ((MovePoint == 5 || MovePoint == 10) && comShortcut == 0 && nowPlayerName == "컴퓨터")
            {
                return true;

            }else if (MovePoint == 8 && userShortcut == 1 && nowPlayerName == "사용자")
            {
                return true;
            }
            else if (MovePoint == 8 && comShortcut == 1 && nowPlayerName == "컴퓨터")
            {
                return true;
            }
            return false;
        }

        //3)현재위치에서 지름길 사용이 가능할 때, 지름길을 이용하는 함수.
        public static void using_shortcut(int MovePoint, string nowPlayerName)
        {

            if ((MovePoint == 5 || MovePoint == 10) && userShortcut == 0 && nowPlayerName == "사용자")
            {
                userShortcut = MovePoint == 5 ? 1 : 2; // 사용자의 통과하는 지름길1,2중 하나 저장
                userShortcutCount = 1; //사용자의 지름길 이용횟수
                userTotalPoint = 16; //총 완주해야할 칸수
                Console.WriteLine($"지름길을 이용합니다! ");
                Console.WriteLine($"현재 지름길 이용횟수 : 1 ");
            }
            else if ((MovePoint == 5 || MovePoint == 10) && comShortcut == 0 && nowPlayerName == "컴퓨터")
            {
                comShortcut = MovePoint == 5 ? 1 : 2; // 컴퓨터의 통과하는 지름길1,2중 하나 저장
                comShortcutCount = 1; //컴퓨터의 지름길 이용횟수
                comTotalPoint = 16; //총 완주해야할 칸수
                Console.WriteLine($"지름길을 이용합니다! ");
                Console.WriteLine($"현재 지름길 이용횟수 : 1 ");
            }
            else if (MovePoint == 8 && userShortcut == 1 && nowPlayerName == "사용자") // 1에서 왔고, 중앙 지름길이라면
            {
                userShortcutCount = 2; //사용자의 지름길 이용횟수
                userTotalPoint = 11; //총 완주해야할 칸수
                Console.WriteLine($"지름길을 이용합니다! ");
                Console.WriteLine($"현재 지름길 이용횟수 : 2 ");
            }
            else if (MovePoint == 8 && comShortcut == 1 && nowPlayerName == "컴퓨터") // 1에서 왔고, 중앙 지름길이라면
            {
                comShortcutCount = 2; //컴퓨터의 지름길 이용횟수
                comTotalPoint = 11; //총 완주해야할 칸수
                Console.WriteLine($"지름길을 이용합니다! ");
                Console.WriteLine($"현재 지름길 이용횟수 : 2 ");
            }
        }

        //4)지름길 사용 가능한 위치에서 빽도가 나왔을때, 이미 줄어든 가야 할 칸수를 다시 보상해주는 함수.
        private void back_shortcut(string playerName) // 적용해야함.
        {
            if (playerName == "사용자")
            {
                if (userShortcut == 1) // 1번 지름길 이용 중
                {
                    // 지름길의 시작 부분보다 앞으로 갔는지 확인
                    if (userShortcutBoardFirst > 0)
                    {
                        userShortcutBoardFirst--;
                    }
                    else // 지름길 시작 부분에서 빽도를 던졌을 경우
                    {
                        // 주요 경로로 돌아감
                        userShortcut = 0;
                        userBasicBoard -= 1;
                    }
                }
                else if (userShortcut == 2) // 2번 지름길 이용 중
                {
                    if (userShortcutBoardSecond > 0)
                    {
                        userShortcutBoardSecond--;
                    }
                    else
                    {
                        userShortcut = 0;
                        userBasicBoard -= 1;
                    }
                }
                else // 지름길을 사용하지 않고 기본 경로에 있을 때
                {
                    if (userBasicBoard > 0)
                    {
                        userBasicBoard--;
                    }
                }
            }
            else if (playerName == "컴퓨터")
            {
                if (comShortcut == 1)
                {
                    if (comShortcutBoardFirst > 0)
                    {
                        comShortcutBoardFirst--;
                    }
                    else
                    {
                        comShortcut = 0;
                        comBasicBoard -= 1;
                    }
                }
                else if (comShortcut == 2)
                {
                    if (comShortcutBoardSecond > 0)
                    {
                        comShortcutBoardSecond--;
                    }
                    else
                    {
                        comShortcut = 0;
                        comBasicBoard -= 1;
                    }
                }
                else
                {
                    if (comBasicBoard > 0)
                    {
                        comBasicBoard--;
                    }
                }
            }
        }

        //5)윷이 나온만큼 이동했을때, 상대방 말을 잡을 수 있는 상태인지 판별하는 함수
        public static bool is_catch_available(string PlayerName)
        {

            if ((userBasicBoard != 0 && comBasicBoard != 0 && userBasicBoard == comBasicBoard)
                || (userShortcutBoardFirst != 0 && comShortcutBoardFirst != 0 && userShortcutBoardFirst == comShortcutBoardFirst)
                || (userShortcutBoardSecond != 0 && comShortcutBoardSecond != 0 && userShortcutBoardSecond == comShortcutBoardSecond))
            {
                if (PlayerName == "사용자")
                {
                    comShortcut = 0;
                    comShortcutCount = 0;
                    comTotalPoint = 20;
                    comBasicBoard = 0;
                    comShortcutBoardFirst = 0;
                    comShortcutBoardSecond = 0;
                    comMovePoint = 0;
                    Console.WriteLine($"상대방 말을 잡았습니다! 윷을 한 번 더 던집니다! %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                    Console.WriteLine($"당신의 차례입니다! %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                    Console.WriteLine($"윷을 던지려면 1을 입력하세요! %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                }
                else if (PlayerName == "컴퓨터")
                {
                    userShortcut = 0;
                    userShortcutCount = 0;
                    userTotalPoint = 20;
                    userBasicBoard = 0;
                    userShortcutBoardFirst = 0;
                    userShortcutBoardSecond = 0;
                    userMovePoint = 0;
                    Console.WriteLine($"당신의 말이 잡혔습니다! 컴퓨터가 윷을 한 번 더 던집니다! ???????????????????????????????????????????");
                    Console.WriteLine($"컴퓨터의 차례입니다! ???????????????????????????????????????????");
                    Console.WriteLine($"윷을 던지게 하려면 1을 입력하세요! ???????????????????????????????????????????");
                }
                else { Console.WriteLine($"오류"); }
                return true;
            }
            else 
            {
                return false;
            }

        }
    }
}
